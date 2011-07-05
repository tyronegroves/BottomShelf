using System.Collections.Generic;

namespace SimpleHost
{
    public interface IHostedServiceScanner
    {
        IEnumerable<HostedService> Scan(string directoryPath);
    }
}