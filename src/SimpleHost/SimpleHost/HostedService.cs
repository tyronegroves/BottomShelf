using System;
using SimpleHost.Core;

namespace SimpleHost
{
    [Serializable]
    public class HostedService
    {
        public HostedService(string assemblyFilePath, string assemblyName, string serviceType, string serviceBaseDirectory)
        {
            AssemblyFilePath = assemblyFilePath;
            AssemblyName = assemblyName;
            ServiceType = serviceType;
            ServiceBaseDirectory = serviceBaseDirectory;
        }

        public string AssemblyFilePath { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceType { get; set; }
        public AppDomain LoadedAppDomain { get; set; }
        public string ServiceBaseDirectory { get; set; }
        public HostedServiceBase HostedServiceInstance { get; set; }

        public void Start()
        {
            HostedServiceInstance.Start();
        }

        public void Stop()
        {
            HostedServiceInstance.Stop();
            
            if(LoadedAppDomain != null)
                AppDomain.Unload(LoadedAppDomain);

            var disposable = HostedServiceInstance as IDisposable;
            if(disposable != null)
                disposable.Dispose();

            HostedServiceInstance = null;

            GC.Collect();
        }
    }
}