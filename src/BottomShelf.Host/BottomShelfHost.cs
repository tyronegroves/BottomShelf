using System.Collections.Generic;
using System.IO;
using FileSystemWatcher = BottomShelf.Host.Monitoring.FileSystemWatcher;

namespace BottomShelf.Host
{
    public class BottomShelfHost
    {
        private readonly ILog logger = LogManager.GetLog(typeof(BottomShelfHost));
        private List<HostedService> hostedServices;
        private FileSystemWatcher watcher;

        public void Start(CommandLineParameters parameters)
        {
            logger.Info("Starting BottomShelf host.");

            var servicesDirectory = Path.GetFullPath(parameters.ServicesDirectory);
            watcher = new FileSystemWatcher(servicesDirectory, parameters.FileSystemPoll);

            hostedServices = new AssemblyScanner().Scan(servicesDirectory);
            hostedServices.ForEach(hs => StartHostedService(hs, watcher));

            watcher.Start();

            logger.Info("Started monitoring changes in '{0}'.", servicesDirectory);
        }

        public void Stop()
        {
            logger.Info("Stopping all hosted services.");
            hostedServices.ForEach(hs => hs.Stop());
            logger.Info("Stopped all hosted services.");
        }

        private void StartHostedService(HostedService hostedService, FileSystemWatcher watcher)
        {
            logger.Info("Starting service '{0}'.", hostedService.HostedServiceTypeName);
            hostedService.Start(watcher, () => hostedServices.Remove(hostedService));
            logger.Info("Started service '{0}'.", hostedService.HostedServiceTypeName);
        }
    }
}