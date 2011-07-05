namespace SimpleHost
{
    public interface IHostedServiceManager
    {
        void LoadFromDirectory(string path);
        void UnloadAllHostedServices();
    }
}