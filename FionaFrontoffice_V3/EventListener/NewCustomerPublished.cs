using FionaFrontoffice_V3.Api.Command;
using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.EventListener
{
    internal class NewCustomerPublished : DomainEvent
    {
        public AddressDto Address { get; internal set; }
    }
}