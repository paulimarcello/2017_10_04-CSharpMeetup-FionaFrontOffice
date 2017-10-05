using System;
using FionaFrontoffice_V3.Domain;
using FionaFrontoffice_V3.Domain.AccountManagerDetection;
using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.ExternalSystems
{
    internal class SendInformationServiceWrapper : ISendInformationService
    {
        private readonly IDomainEventPublisher _domainEventPublisher;


        public SendInformationServiceWrapper(IDomainEventPublisher domainEventPublisher)
        {
            _domainEventPublisher = domainEventPublisher;
        }


        public void InformAccountManager(AccountManager accountManager)
        {
            Console.WriteLine("AccountManager informed");

            _domainEventPublisher.Publish(new AccountManagerInformed());
        }

        public void InformCustomer(Customer customer)
        {
            Console.WriteLine("Customer informed");

            _domainEventPublisher.Publish(new CustomerInformed());
        }
    }
}