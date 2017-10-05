namespace FionaFrontoffice_V3.common
{
    public interface IHandle<T> where T : Command
    {
        void Handle(T command);
    }
}