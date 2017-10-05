using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain
{
    internal class NewResidenceApplied : DomainEvent
    {
        public CustomerId CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Address NewResidence { get; private set; }
        public Address OldResidence { get; private set; }


        public NewResidenceApplied(CustomerId customerId, Customer customer, Address oldResidence, Address newResidence)
        {
            CustomerId = customerId;
            Customer = customer;
            OldResidence = oldResidence;
            NewResidence = newResidence;
        }
    }
}