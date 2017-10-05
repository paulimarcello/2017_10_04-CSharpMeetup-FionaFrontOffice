using System;
using System.Collections.Generic;
using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.Infrastructure
{
    internal class DomainEventPublisher : IDomainEventPublisher
    {
        private Dictionary<Type, List<Action<DomainEvent>>> _subscribers = new Dictionary<Type, List<Action<DomainEvent>>>();


        public void Publish(DomainEvent @event)
        {
            if (_subscribers.TryGetValue(@event.GetType(), out List<Action<DomainEvent>> allRegisteredHandlers))
            {
                foreach (var handle in allRegisteredHandlers)
                {
                    handle(@event);
                }
            }
        }

        public void SubscribeOn<T>(Action<T> eventHandler) where T : DomainEvent
        {
            if (!_subscribers.TryGetValue(typeof(T), out List<Action<DomainEvent>> subscriberList))
            {
                subscriberList = new List<Action<DomainEvent>>();
                _subscribers.Add(typeof(T), subscriberList);
            }

            subscriberList.Add((@event) => eventHandler((T)@event));
        }
    }
}