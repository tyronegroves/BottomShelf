using System;
using System.Collections.Generic;

namespace BottomShelf.Host
{
    public class AssemblyScanner
    {
        private readonly ILog logger = LogManager.GetLog(typeof(AssemblyScanner));

        public List<HostedService> Scan(string servicesDirectory)
        {
            logger.Info("Scanning for hosted services in '{0}'", servicesDirectory);

            return new List<HostedService>();
        }
    }
}