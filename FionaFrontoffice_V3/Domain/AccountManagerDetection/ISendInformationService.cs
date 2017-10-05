namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal interface ISendInformationService
    {
        void InformCustomer(Customer customer);
        void InformAccountManager(AccountManager accountManager);
    }
}