using System;
using System.Collections.Generic;
using FionaFrontoffice_V3.common;
using FionaFrontoffice_V3.Domain;
using Newtonsoft.Json;

namespace FionaFrontoffice_V3.Persistence
{
    internal class InMemoryCustomerRepository : ICustomerRepository
    {
        public IGlobalTransaction GlobalTransaction { get; private set; }

        private readonly IDomainEventPublisher _domainEventPublisher;
        private Dictionary<string, string> _bullshitDatabase = new Dictionary<string, string>();


        public InMemoryCustomerRepository(IGlobalTransaction globalTransaction, IDomainEventPublisher domainEventPublisher)
        {
            GlobalTransaction = globalTransaction;

            _domainEventPublisher = domainEventPublisher;
        }


        public Customer LoadById(CustomerId customerId)
        {
            if (_bullshitDatabase.TryGetValue(customerId.ToString(), out string serializedCustomer))
            {
                var customer = JsonConvert.DeserializeObject<Customer>(serializedCustomer);
                customer.DomainEventPublisher = _domainEventPublisher;

                Console.WriteLine(String.Format("Customer {0} from DB loaded", customerId));

                return customer;
            }

            throw new Exception(String.Format("Customer with Id {0} not found", customerId.ToString()));
        }

        public void SaveCustomer(Customer customer)
        {
            //do not commit here - ok, that's hard when it's an inmemory-db ;-) but in real life...
            if (_bullshitDatabase.ContainsKey(customer.Id.ToString()))
            {
                _bullshitDatabase.Remove(customer.Id.ToString());
            }

            var serializedCustomer = JsonConvert.SerializeObject(customer);
            _bullshitDatabase.Add(customer.Id.ToString(), serializedCustomer);

            Console.WriteLine(String.Format("Customer {0} in DB updated - no commit yet", customer.Id));
        }
    }
}