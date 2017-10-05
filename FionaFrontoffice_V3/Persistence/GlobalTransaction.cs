using System;

namespace FionaFrontoffice_V3.Persistence
{
    internal interface IGlobalTransaction
    {
        bool IsActive();
        void Commit();
        void Rollback();
    }

    internal class GlobalTransaction : IGlobalTransaction
    {
        private static volatile IGlobalTransaction _globalTransaction;
        private static object _lock = new object();

        protected bool _isActive;


        public static IGlobalTransaction GetGlobalTransaction()
        {
            if (_globalTransaction == null)
            {
                lock (_lock)
                {
                    if (_globalTransaction == null)
                    {
                        _globalTransaction = new GlobalTransaction();
                    }
                }
            }

            return _globalTransaction;
        }

        private GlobalTransaction()
        {
            _isActive = true;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void Commit()
        {
            //the real commit to you db for all repos is here
            Console.WriteLine("global transaction commited");
        }

        public void Rollback()
        {
            _isActive = false;
            Console.WriteLine("global transaction rolled back");
            //the real realback on your db for all repos is here
        }


    }
}
