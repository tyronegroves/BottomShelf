using System;
using SimpleHost.Monitoring.Events;

namespace SimpleHost.Monitoring
{
    [Serializable]
    public class QueuedFileSystemEventArgs : EventArgs
    {
        public QueuedFileSystemEventArgs(QueuedFileSystemEvent queuedFileSystemEvent)
        {
            QueuedFileSystemEvent = queuedFileSystemEvent;
        }

        public QueuedFileSystemEvent QueuedFileSystemEvent { get; private set; }
    }
}