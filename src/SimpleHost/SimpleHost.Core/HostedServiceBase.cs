using System;

namespace SimpleHost.Core
{
    public abstract class HostedServiceBase : MarshalByRefObject
    {
        public abstract void Start();
        public abstract void Stop();
    }
}