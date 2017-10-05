namespace FionaFrontoffice_V3.common
{
    internal interface IListenOn<T> where T : DomainEvent
    {
        void Handle(T @event);
    }
}