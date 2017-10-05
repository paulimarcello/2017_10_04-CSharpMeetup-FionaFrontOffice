using System;

namespace FionaFrontoffice_V3.common
{
    internal abstract class DomainEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccuredOn { get; private set; }


        public DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
        }
    }
}