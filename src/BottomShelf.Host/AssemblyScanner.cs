using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BottomShelf.Host
{
    public class AssemblyScanner
    {
        private readonly ILog logger = LogManager.GetLog(typeof(AssemblyScanner));

        public List<HostedService> Scan(string servicesDirectory)
        {
            logger.Info("Scanning for hosted services in '{0}'.", servicesDirectory);

            AppDomain appDomain = null;
            var assemblyName = Assembly.GetExecutingAssembly().FullName;
            var remoteAssemblyScannerTypeName = typeof(RemoteAssemblyScanner).FullName;

            try
            {
                // Create an AppDomain and create a scanner that will run inside of it.  We must use a 
                // child AppDomain so we can unload the assemblies that are being scanned.
                appDomain = AppDomain.CreateDomain("AssemblyScanner");
                var remoteAssemblyScanner = (RemoteAssemblyScanner)appDomain.CreateInstanceAndUnwrap(assemblyName, remoteAssemblyScannerTypeName, false, BindingFlags.Default, null, new[] {logger}, null, new object[] {});

                return remoteAssemblyScanner.Scan(servicesDirectory);
            }
            finally
            {
                if(appDomain != null)
                    AppDomain.Unload(appDomain);
            }
        }

        private class RemoteAssemblyScanner : MarshalByRefObject
        {
            private readonly ILog logger;

            public RemoteAssemblyScanner(ILog logger)
            {
                this.logger = logger;
            }

            public List<HostedService> Scan(string servicesDirectory)
            {
                var hostedServices = new List<HostedService>();
                var files = GetFilesToScan(servicesDirectory);
                var hostedServiceBaseType = typeof(HostedServiceBase);

                foreach(var file in files)
                {
                    logger.Info("Scanning '{0}' for services.", file);

                    try
                    {
                        var assembly = Assembly.LoadFrom(file);

                        logger.Info("Loaded assembly '{0}'.", assembly.FullName);

                        var services = (from type in assembly.GetTypes()
                                        let baseType = type.BaseType
                                        where baseType != null
                                        where baseType.FullName == hostedServiceBaseType.FullName
                                        where baseType.AssemblyQualifiedName == hostedServiceBaseType.AssemblyQualifiedName
                                        select new HostedService(type.FullName, assembly.GetName(), assembly.Location)).ToList();

                        services.ForEach(hs => logger.Info("Found service type '{0}'.", hs.HostedServiceTypeName));

                        hostedServices.AddRange(services);
                    }
                    catch(FileLoadException)
                    {
                        logger.Warn("Could not load '{0}'.", file);
                        continue;
                    }
                    catch(BadImageFormatException)
                    {
                        logger.Warn("Could not load '{0}'.  The file is not a valid assembly.", file);
                        continue;
                    }
                }

                return hostedServices;
            }

            private static IEnumerable<string> GetFilesToScan(string servicesDirectory)
            {
                return (from directory in Directory.GetDirectories(servicesDirectory)
                        from file in Directory.GetFiles(directory, "*.dll")
                        select file).ToArray();
            }
        }
    }
}