using FionaFrontoffice_V3.Api.Command;
using FionaFrontoffice_V3.common;
using FionaFrontoffice_V3.Domain;
using FionaFrontoffice_V3.Infrastructure;
using FionaFrontoffice_V3.Persistence;
using FionaFrontoffice_V3.Utils;
using System;

namespace FionaFrontoffice_V3.Api
{
    internal class CustomerCommandService :
            ICustomerCommandService
          , IHandle<ApplyNewCustomerCommand>
          , IHandle<AnnounceNewResidenceCommand>
          , IHandle<CancelContractCommand>
    {
        private readonly IGlobalTransaction _globalTransaction;
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly ICustomerRepository _customerRepository;


        public CustomerCommandService(IGlobalTransaction globalTransaction, DomainEventPublisher domainEventPublisher, ICustomerRepository customerRepository)
        {
            _globalTransaction = globalTransaction;
            _domainEventPublisher = domainEventPublisher;
            _customerRepository = customerRepository;
        }


        public void Handle(ApplyNewCustomerCommand command)
        {
            var newCustomer = Customer.New(_domainEventPublisher, Mapper.GetAddressFromAdressDto(command.Residence));

            _customerRepository.SaveCustomer(newCustomer);

            CommitGlobalTransactionIfSuccessfull();
        }

        public void Handle(AnnounceNewResidenceCommand command)
        {
            var customerId = new CustomerId(new Guid(command.CustomerId));

            var customer = _customerRepository.LoadById(customerId);

            customer.AnnounceNewResidence(Mapper.GetAddressFromAdressDto(command.NewResidence));

            _customerRepository.SaveCustomer(customer);

            CommitGlobalTransactionIfSuccessfull();
        }

        public void Handle(CancelContractCommand command)
        {
            // ...

            CommitGlobalTransactionIfSuccessfull();
        }


        private void CommitGlobalTransactionIfSuccessfull()
        {
            if (_globalTransaction.IsActive())
            {
                _globalTransaction.Commit();
            }
        }
    }
}