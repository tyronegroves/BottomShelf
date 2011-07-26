using System.Collections.Generic;
using System.IO;

namespace BottomShelf.Host
{
    public class BottomShelfHost
    {
        private readonly ILog logger = LogManager.GetLog(typeof(BottomShelfHost));
        private List<HostedService> hostedServices;

        public void Start(CommandLineParameters commandLineParameters)
        {
            logger.Info("Starting BottomShelf host");

            var servicesDirectory = Path.GetFullPath(commandLineParameters.ServicesDirectory);
            hostedServices = new AssemblyScanner().Scan(servicesDirectory);
            hostedServices.ForEach(hs => hs.Start());
        }

        public void Stop()
        {
            logger.Info("Stopping BottomShelf host");

            hostedServices.ForEach(hs => hs.Stop());
        }
    }
}