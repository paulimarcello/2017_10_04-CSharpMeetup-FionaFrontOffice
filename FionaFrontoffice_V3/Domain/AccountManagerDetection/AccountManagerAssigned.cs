using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class AccountManagerAssigned : DomainEvent
    {
        public Customer Customer { get; private set; }
        public AccountManager AccountManager { get; private set; }


        public AccountManagerAssigned(Customer customer, AccountManager accountManager)
        {
            Customer = customer;
            AccountManager = accountManager;
        }
    }
}