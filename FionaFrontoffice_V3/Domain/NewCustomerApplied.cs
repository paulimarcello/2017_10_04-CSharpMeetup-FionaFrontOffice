using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain
{
    internal class NewCustomerApplied : DomainEvent
    {
        public CustomerId CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Address Address { get; private set; }


        public NewCustomerApplied(CustomerId customerId, Customer customer, Address _address)
        {
            CustomerId = customerId;
            Customer = customer;
            Address = _address;
        }
    }
}