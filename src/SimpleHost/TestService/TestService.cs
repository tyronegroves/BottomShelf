using System;
using System.Threading;
using SimpleHost.Core;

namespace TestService
{
    public class TestService : HostedServiceBase
    {
        private Timer timer;

        public override void Start()
        {
            timer = new Timer(OnTimer, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public override void Stop()
        {
            timer.Dispose();
            Console.WriteLine("Stopping service");
        }

        private void OnTimer(object state)
        {
            Console.WriteLine("Checking in at {0}", DateTime.Now);
        }
    }
}