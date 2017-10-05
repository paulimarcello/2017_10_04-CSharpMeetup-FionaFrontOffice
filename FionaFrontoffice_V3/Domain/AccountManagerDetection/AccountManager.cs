using FionaFrontoffice_V3.common;
using System.Collections.Generic;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class AccountManager
    {
        private readonly IDomainEventPublisher _domainEventPublisher;

        public AccountManagerId Id { get; private set; }
        private List<CustomerId> _myGuidedCustomers = new List<CustomerId>();


        public AccountManager(IDomainEventPublisher domainEventPublisher, AccountManagerId id, List<CustomerId> myGuidedCustomers)
        {
            Id = id;
            _domainEventPublisher = domainEventPublisher;
            _myGuidedCustomers = myGuidedCustomers;
        }

        public void CareCustomer(Customer customer)
        {
            _myGuidedCustomers.Add(customer.Id);

            _domainEventPublisher.Publish(new CustomerInCustomerbaseIncluded(this));

            //customer.AssignAccountManager(this);
        }
    }
}