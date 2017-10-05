using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal interface IAccountManagerLocator
    {
        void DetermineClosestAccountManager(IDomainEventPublisher domainEventPublisher, Address address);
    }
}