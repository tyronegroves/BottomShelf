using System;

namespace SimpleHost.Monitoring.Events
{
    public abstract class QueuedFileSystemEvent
    {
        protected QueuedFileSystemEvent()
        {
            EventDate = DateTime.Now;
        }

        public DateTime EventDate { get; private set; }
    }
}