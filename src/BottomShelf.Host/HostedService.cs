using System;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using FileSystemWatcher = BottomShelf.Host.Monitoring.FileSystemWatcher;

namespace BottomShelf.Host
{
    [Serializable]
    public class HostedService
    {
        private static ILog logger = LogManager.GetLog<HostedServiceBase>();
        private readonly AssemblyName assemblyName;
        private readonly string assemblyFile;
        private AppDomain appDomain;
        private HostedServiceBase hostedServiceInstance;

        public HostedService(string hostedServiceTypeName, AssemblyName assemblyName, string assemblyFile)
        {
            this.assemblyName = assemblyName;
            this.assemblyFile = assemblyFile;
            HostedServiceTypeName = hostedServiceTypeName;
        }

        public void Start(FileSystemWatcher watcher, Action removeHostedService)
        {
            var appDomainSetup = new AppDomainSetup { ConfigurationFile = string.Format("{0}.config", assemblyFile), ShadowCopyFiles = "true", ApplicationBase = Path.GetDirectoryName(assemblyFile)};
            appDomain = AppDomain.CreateDomain(string.Format("BottomShelf.Host - {0}", HostedServiceTypeName), new Evidence(), appDomainSetup);

            try
            {
                hostedServiceInstance = (HostedServiceBase)appDomain.CreateInstanceAndUnwrap(assemblyName.FullName, HostedServiceTypeName);
                hostedServiceInstance.Start();
            }
            catch(Exception exception)
            {
                logger.Error(exception);
                removeHostedService();
            }
        }

        public void Stop()
        {
            hostedServiceInstance.Stop();
            AppDomain.Unload(appDomain);
        }

        public string HostedServiceTypeName { get; private set; }
    }
}