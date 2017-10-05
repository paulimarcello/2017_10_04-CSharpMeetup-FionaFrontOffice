using FionaFrontoffice_V3.Api;
using FionaFrontoffice_V3.Api.Query;
using FionaFrontoffice_V3.Domain.AccountManagerDetection;
using FionaFrontoffice_V3.EventListener;
using FionaFrontoffice_V3.ExternalSystems;
using FionaFrontoffice_V3.Infrastructure;
using FionaFrontoffice_V3.Persistence;

namespace FionaFrontoffice_V3
{
    internal class App
    {
        // just for demo public
        public DomainEventPublisher BullshitEnterpriseServiceBus { get; private set; }

        // this is the entry point of our app - so should be public
        public ICustomerCommandService CustomerCommandService;
        public ISearchCustomerView SearchCustomerView;


        // internal event listeners
        private readonly CustomerPublishedEventListener _customerPublishedEventListener;

        // internal process managers
        public AccountManagerDetectionProcessManager CustomerAccountManagerMatchingProcessManager { get; private set; }

        // internal domain services
        private readonly IAccountManagerLocator _accountManagerLocator;

        // persistence layer
        private readonly InMemoryCustomerRepository _customerRepository;
        private readonly InMemoryAccountManagerRepository _accountManagerRepository;

        // external system wrappers
        private readonly ISendInformationService _sendInformationService;



        // hosts the app and acts as the composition root
        public App()
        {
            IGlobalTransaction globalTransaction = GlobalTransaction.GetGlobalTransaction();

            BullshitEnterpriseServiceBus = new DomainEventPublisher();

            _customerRepository = new InMemoryCustomerRepository(globalTransaction, BullshitEnterpriseServiceBus);

            _accountManagerRepository = new InMemoryAccountManagerRepository(globalTransaction);

            _sendInformationService = new SendInformationServiceWrapper(BullshitEnterpriseServiceBus);

            _accountManagerLocator = new AccountManagerLocator();


            CustomerCommandService = new CustomerCommandService(globalTransaction, BullshitEnterpriseServiceBus, _customerRepository);

            _customerPublishedEventListener = new CustomerPublishedEventListener(BullshitEnterpriseServiceBus, CustomerCommandService);

            CustomerAccountManagerMatchingProcessManager = new AccountManagerDetectionProcessManager(globalTransaction
                                                                                                        , BullshitEnterpriseServiceBus
                                                                                                        , _accountManagerLocator
                                                                                                        , _sendInformationService
                                                                                                        , _customerRepository
                                                                                                        , _accountManagerRepository);

            SearchCustomerView = new CustomerActualResidenceViewUpdater(BullshitEnterpriseServiceBus);
        }
    }
}