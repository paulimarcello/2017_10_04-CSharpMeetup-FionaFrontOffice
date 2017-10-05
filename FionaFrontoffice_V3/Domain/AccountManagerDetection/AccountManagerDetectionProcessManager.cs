using System;
using FionaFrontoffice_V3.common;
using FionaFrontoffice_V3.Persistence;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class AccountManagerDetectionProcessManager
    {
        private readonly IGlobalTransaction _globalTransaction;
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly IAccountManagerLocator _accountManagerLocator;
        private readonly ISendInformationService _sendInformationService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountManagerRepository _accountManagerRepository;

        // internal state of process-manager
        private bool _isCustomerInformed;
        private bool _isAccountManagerInformed;
        private Customer _customer;
        private AccountManager _closestAccountManager;


        // @Transactional
        public AccountManagerDetectionProcessManager(IGlobalTransaction globalTransaction
                                                     , IDomainEventPublisher domainEventPublisher
                                                     , IAccountManagerLocator accountManagerLocator
                                                     , ISendInformationService sendInformationService
                                                     , ICustomerRepository customerRepository
                                                     , IAccountManagerRepository accountManagerRepository)
        {
            _globalTransaction = globalTransaction;
            _domainEventPublisher = domainEventPublisher;
            _accountManagerLocator = accountManagerLocator;
            _sendInformationService = sendInformationService;
            _customerRepository = customerRepository;
            _accountManagerRepository = accountManagerRepository;

            // triggers to start the process
            _domainEventPublisher.SubscribeOn<NewCustomerApplied>(DetectClosestAccountManager);
            _domainEventPublisher.SubscribeOn<NewResidenceApplied>(DetectClosestAccountManager);

            // customer path
            _domainEventPublisher.SubscribeOn<ClosestAccountManagerDetected>(AssignAccountManager);
            _domainEventPublisher.SubscribeOn<AccountManagerAssigned>(InformCustomer);
            _domainEventPublisher.SubscribeOn<AccountManagerInformed>(RaiseAccountManagerDetectedWhenCustomerInformed);

            // account manager path
            _domainEventPublisher.SubscribeOn<ClosestAccountManagerDetected>(CareCustomer);
            _domainEventPublisher.SubscribeOn<CustomerInCustomerbaseIncluded>(InformAccountManager);
            _domainEventPublisher.SubscribeOn<CustomerInformed>(RaiseAccountManagerDetectedWhenCustomerInformed);
        }


        private void DetectClosestAccountManager(NewCustomerApplied @event)
        {
            Console.WriteLine("new customer applied -> detect closest account manager");

            _customer = @event.Customer;

            _accountManagerLocator.DetermineClosestAccountManager(_domainEventPublisher, @event.Address);
        }

        private void AssignAccountManager(ClosestAccountManagerDetected @event)
        {
            Console.WriteLine("closest account manager detected -> assign account manager");

            _closestAccountManager = @event.AccountManager;

            _customer.AssignAccountManager(_closestAccountManager);
        }

        private void InformCustomer(AccountManagerAssigned @event)
        {
            Console.WriteLine("account manager assigned -> inform customer");

            // be careful in real life when you're going to invoke other systems which is not under your transaction control!
            // when your own (database) transaction will fail, you have to compensate these calls!
            // this is just a demo which doesn't focus on that point
            _sendInformationService.InformCustomer(@event.Customer);
        }

        private void RaiseAccountManagerDetectedWhenCustomerInformed(AccountManagerInformed @event)
        {
            _isAccountManagerInformed = true;

            if (_isCustomerInformed)
            {
                EndProcess();
            }
        }


        private void DetectClosestAccountManager(NewResidenceApplied @event)
        {

            Console.WriteLine("new residence applied -> detect closest account manager");

            _customer = @event.Customer;

            _accountManagerLocator.DetermineClosestAccountManager(_domainEventPublisher, @event.NewResidence);
        }

        private void CareCustomer(ClosestAccountManagerDetected @event)
        {
            Console.WriteLine("closest account manager detected -> care customer");

            _closestAccountManager = @event.AccountManager;

            _closestAccountManager.CareCustomer(_customer);
        }

        private void InformAccountManager(CustomerInCustomerbaseIncluded @event)
        {
            Console.WriteLine("customer in customerbase included -> inform account manager");

            // be careful in real life when you're going to invoke other systems which is not under your transaction control!
            // when your own (e.g. database) transaction will fail, you have to compensate these calls!
            // this is just a demo which doesn't focus on that point
            _sendInformationService.InformAccountManager(@event.AccountManager);
        }

        private void RaiseAccountManagerDetectedWhenCustomerInformed(CustomerInformed @event)
        {
            _isCustomerInformed = true;

            if (_isAccountManagerInformed)
            {
                EndProcess();
            }
        }


        private void EndProcess()
        {
            Console.WriteLine("end of process tasks now ");
            try
            {
                _customerRepository.SaveCustomer(_customer);
                _accountManagerRepository.SaveAccountManager(_closestAccountManager);
                try
                {
                    Console.WriteLine("account manager detected");
                    _domainEventPublisher.Publish(new AccountManagerDetected());
                }
                catch (Exception e)
                {
                    // consider invoking a compensation call of "InformAccountManager" and "InformCustomer" here
                    _globalTransaction.Rollback();
                    Console.WriteLine("Transaction failed publishing final event. DB-Transaction rolled back " + e);
                    throw;
                }
            }
            catch (Exception e)
            {
                // consider invoking a compensation call of "InformAccountManager" and "InformCustomer" here again
                _globalTransaction.Rollback();
                Console.WriteLine("Transaction failed while persisting" + e);
            }
        }


        // this is just for demo. In real life the process manager would be generated new for every incoming command
        public void Reset()
        {
            _isAccountManagerInformed = false;
            _isCustomerInformed = false;
            _customer = null;
            _closestAccountManager = null;
        }
    }
}