using System;
using Newtonsoft.Json;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    public struct AccountManagerId
    {
        [JsonProperty]
        private readonly Guid _accountManagerId;


        public AccountManagerId(Guid accountManagerId)
        {
            _accountManagerId = accountManagerId;
        }

        public override string ToString()
        {
            return _accountManagerId.ToString();
        }
    }
}