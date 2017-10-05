using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class ClosestAccountManagerDetected : DomainEvent
    {
        public AccountManager AccountManager { get; private set; }


        public ClosestAccountManagerDetected(AccountManager accountManager)
        {
            AccountManager = accountManager;
        }
    }
}