using System;

namespace FionaFrontoffice_V3.common
{
    internal interface IDomainEventPublisher
    {
        void SubscribeOn<T>(Action<T> eventHandler) where T : DomainEvent;
        void Publish(DomainEvent @event);
    }
}