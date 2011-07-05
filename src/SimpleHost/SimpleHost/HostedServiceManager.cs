using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleHost
{
    public class HostedServiceManager : IHostedServiceManager
    {
        private readonly IHostedServiceScanner hostedServiceScanner;
        private readonly IHostedServiceLoader hostedServiceLoader;
        private readonly List<HostedService> loadedHostedServices;

        public HostedServiceManager(IHostedServiceScanner hostedServiceScanner, IHostedServiceLoader hostedServiceLoader)
        {
            this.hostedServiceScanner = hostedServiceScanner;
            this.hostedServiceLoader = hostedServiceLoader;
            loadedHostedServices = new List<HostedService>();
        }

        public void LoadFromDirectory(string path)
        {
            var hostedServiceInfos = hostedServiceScanner.Scan(path);
            foreach(var hostedServiceInfo in hostedServiceInfos)
            {
                hostedServiceLoader.Load(hostedServiceInfo);
                loadedHostedServices.Add(hostedServiceInfo);
            }

            hostedServiceInfos
                .ToList()
                .ForEach(hs => hs.Start());
        }

        public void UnloadAllHostedServices()
        {
            loadedHostedServices
                .ForEach(hs => hs.Stop());

            loadedHostedServices.Clear();
        }

        public void UnloadAllInDirectory(string fullDirectoryPath)
        {
            var hostedServices = loadedHostedServices.Where(hs => hs.ServiceBaseDirectory == fullDirectoryPath);
            hostedServices.ToList()
                .ForEach(hs=>hs.Stop());

            hostedServices.ToList()
                .ForEach(hs => loadedHostedServices.Remove(hs));
        }
    }
}