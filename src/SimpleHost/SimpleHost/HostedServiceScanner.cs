using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleHost.Core;

namespace SimpleHost
{
    public class HostedServiceScanner : IHostedServiceScanner
    {
        private static readonly string AppDomainScannerTypeName = typeof(AppDomainScanner).FullName;
        private static readonly string SimpleHostAssemblyFullName = Assembly.GetExecutingAssembly().FullName;

        public IEnumerable<HostedService> Scan(string directoryPath)
        {
            AppDomain appDomain = null;

            try
            {
                // Create an AppDomain and create a scanner that will run inside of it.  We must use a 
                // child AppDomain so we can unload the assemblies that are being scanned.
                appDomain = AppDomain.CreateDomain("HostedServiceDirectoryScanner");
                var scanner = (AppDomainScanner)appDomain.CreateInstanceAndUnwrap(SimpleHostAssemblyFullName, AppDomainScannerTypeName);

                return scanner.Scan(directoryPath);
            }
            finally
            {
                if(appDomain != null)
                    AppDomain.Unload(appDomain);
            }
        }

        private class AppDomainScanner : MarshalByRefObject
        {
            private static readonly string SimpleHostCoreFileName = typeof(HostedServiceBase).Assembly.GetName().Name;
            private static readonly Type HostedServiceType = typeof(HostedServiceBase);

            public IEnumerable<HostedService> Scan(string directoryPath)
            {
                var files = GetFilesToScan(directoryPath);
                var hostedServiceInfos = new List<HostedService>();

                foreach(var file in files)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(file);
                        var serviceInfos = assembly.GetTypes()
                            .Where(type => HostedServiceType.IsAssignableFrom(type))
                            .Select(type => new HostedService(Path.GetFullPath(file), assembly.FullName, type.FullName, directoryPath));
                        
                        hostedServiceInfos.AddRange(serviceInfos);
                    }
                    catch(FileLoadException)
                    {
                        continue;
                    }
                    catch(BadImageFormatException)
                    {
                        continue;
                    }
                }

                return hostedServiceInfos;
            }

            private static IEnumerable<string> GetFilesToScan(string directoryPath)
            {
                var files = Directory.GetFiles(directoryPath, "*.dll");
                return files.Where(file => Path.GetFileNameWithoutExtension(file) != SimpleHostCoreFileName);
            }
        }
    }
}