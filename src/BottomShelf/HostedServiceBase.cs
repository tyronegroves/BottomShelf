using System;

namespace BottomShelf
{
    public abstract class HostedServiceBase : MarshalByRefObject
    {
        public abstract void Start();
        public abstract void Stop();
    }
}