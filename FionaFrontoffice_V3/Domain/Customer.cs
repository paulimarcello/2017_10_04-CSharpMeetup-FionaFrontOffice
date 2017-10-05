using FionaFrontoffice_V3.common;
using Newtonsoft.Json;
using System;
using FionaFrontoffice_V3.Domain.AccountManagerDetection;

namespace FionaFrontoffice_V3.Domain
{
    internal class Customer
    {
        [JsonIgnore]
        public IDomainEventPublisher DomainEventPublisher { get; set; }

        [JsonProperty]
        public CustomerId Id { get; private set; }

        [JsonProperty]
        private AccountManagerId? _accountManagerId;

        [JsonProperty]
        private Address _address;


        public static Customer New(IDomainEventPublisher domainEventPublisher, Address address)
        {
            var newCustomer = new Customer(new CustomerId(Guid.NewGuid()), address, null)
            {
                DomainEventPublisher = domainEventPublisher
            };

            domainEventPublisher.Publish(new NewCustomerApplied(newCustomer.Id, newCustomer, newCustomer._address));

            return newCustomer;
        }

        public Customer(CustomerId id, Address address, AccountManagerId? accountManagerId)
        {
            Id = id;
            _address = address;
            _accountManagerId = accountManagerId;
        }


        public void AnnounceNewResidence(Address newAddress)
        {
            // maybe some domainlogic here

            var oldAddress = _address;
            _address = newAddress;

            DomainEventPublisher.Publish(new NewResidenceApplied(Id, this, oldAddress, _address));
        }

        public void CancelContract()
        {
            // maybe some domainlogic here

            DomainEventPublisher.Publish(new ContractCanceled());
        }

        public void AssignAccountManager(AccountManager accountManager)
        {
            // maybe some domain logic here, e.g. inform previous account manager

            _accountManagerId = accountManager.Id;

            DomainEventPublisher.Publish(new AccountManagerAssigned(this, accountManager));
        }
    }
}