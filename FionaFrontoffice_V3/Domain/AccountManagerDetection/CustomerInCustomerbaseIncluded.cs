using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class CustomerInCustomerbaseIncluded : DomainEvent
    {
        public AccountManager AccountManager { get; private set; }


        public CustomerInCustomerbaseIncluded(AccountManager accountManager)
        {
            AccountManager = accountManager;
        }
    }
}