using System;
using System.Security.Policy;
using SimpleHost.Core;

namespace SimpleHost
{
    public class HostedServiceLoader : IHostedServiceLoader
    {
        public void Load(HostedService hostedServiceInfo)
        {
            var friendlyName = hostedServiceInfo.ServiceType;
            var appDomainSetup = new AppDomainSetup {ShadowCopyFiles = "true", ApplicationBase = hostedServiceInfo.ServiceBaseDirectory, ConfigurationFile = hostedServiceInfo.AssemblyFilePath + ".config"};
            var appDomain = AppDomain.CreateDomain(friendlyName, new Evidence(), appDomainSetup);

            var hostedService =  (HostedServiceBase)appDomain.CreateInstanceAndUnwrap(hostedServiceInfo.AssemblyName, hostedServiceInfo.ServiceType);

            hostedServiceInfo.HostedServiceInstance = hostedService;
            hostedServiceInfo.LoadedAppDomain = appDomain;
        }
    }
}