using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain
{
    internal class ContractCanceled : DomainEvent
    {
        // maybe some more Information about the canceled contract e.g. Reason etc.

        public ContractCanceled()
        {
        }
    }
}