using System;
using FionaFrontoffice_V3.Domain.AccountManagerDetection;

namespace FionaFrontoffice_V3.Persistence
{
    internal class InMemoryAccountManagerRepository : IAccountManagerRepository
    {
        public IGlobalTransaction GlobalTransaction { get; private set; }


        public InMemoryAccountManagerRepository(IGlobalTransaction globalTransaction)
        {
            GlobalTransaction = globalTransaction;
        }


        public void SaveAccountManager(AccountManager account)
        {
            //do not commit here - it's just an update without a commit
            Console.WriteLine("Account Manager saved");
        }
    }
}
