using FionaFrontoffice_V3.common;
using System;
using System.Collections.Generic;

namespace FionaFrontoffice_V3.Domain.AccountManagerDetection
{
    internal class AccountManagerLocator : IAccountManagerLocator
    {                                                                 
        public void DetermineClosestAccountManager(IDomainEventPublisher domainEventPublisher, Address address)
        {
            // something magical happens here - assume that the right account manager was determined
            var closestAccountManager = new AccountManager(domainEventPublisher, new AccountManagerId(Guid.NewGuid()), new List<CustomerId>());

            domainEventPublisher.Publish(new ClosestAccountManagerDetected(closestAccountManager));   
        }
    }
}