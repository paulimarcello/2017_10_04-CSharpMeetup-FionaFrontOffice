using System;
using Newtonsoft.Json;

namespace FionaFrontoffice_V3.Domain
{
    internal struct CustomerId
    {
        [JsonProperty]
        private readonly Guid _customerId;


        public CustomerId(Guid customerId)
        {
            _customerId = customerId;
        }

        public override string ToString()
        {
            return _customerId.ToString();
        }
    }
}