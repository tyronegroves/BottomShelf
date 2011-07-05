using System;
using System.Collections.Concurrent;
using System.IO;
using SimpleHost.Monitoring.Events;

namespace SimpleHost.Monitoring
{
    public class QueueFileSystemWatcher
    {
        private readonly string path;
        private FileSystemWatcher internalWatcher;
        private ConcurrentQueue<QueuedFileSystemEvent> queuedEvents;
        public event EventHandler<QueuedFileSystemEventArgs> FileSystemEventQueued;

        public QueueFileSystemWatcher(string path)
        {
            this.path = path;
            FileSystemEventQueued += (sender, e) => { };
        }

        public void Start()
        {
            queuedEvents = new ConcurrentQueue<QueuedFileSystemEvent>();
            internalWatcher = new FileSystemWatcher(path);
            internalWatcher.Changed += OnChanged;
            internalWatcher.Created += OnCreated;
            internalWatcher.Deleted += OnDeleted;
            internalWatcher.Renamed += OnRenamed;
            
            internalWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            internalWatcher.EnableRaisingEvents = false;
            internalWatcher.Dispose();
        }

        private void EnqueueEvent(QueuedFileSystemEvent queuedFileSystemEvent)
        {
            queuedEvents.Enqueue(queuedFileSystemEvent);
            FileSystemEventQueued(this, new QueuedFileSystemEventArgs(queuedFileSystemEvent));
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if(File.Exists(e.FullPath))
            {
                EnqueueEvent(new FileRenamedEvent { FullFilePath = e.FullPath, OldFilePath = e.OldFullPath, FileName = e.Name, OldFileName = e.OldName });
                return;
            }

            EnqueueEvent(new DirectoryRenamedEvent { FullDirectoryPath = e.FullPath, OldDirectoryPath = e.OldFullPath, DirectoryName = e.Name, OldDirectoryName = e.OldName });
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if(File.Exists(e.FullPath))
            {
                EnqueueEvent(new FileDeletedEvent { FullFilePath = e.FullPath, FileName = e.Name });
                return;
            }

            EnqueueEvent(new DirectoryDeletedEvent { FullDirectoryPath = e.FullPath, DirectoryName = e.Name });
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if(File.Exists(e.FullPath))
            {
                EnqueueEvent(new FileCreatedEvent { FullFilePath = e.FullPath, FileName = e.Name });
                return;
            }

            EnqueueEvent(new DirectoryCreatedEvent { FullDirectoryPath = e.FullPath, DirectoryName = e.Name });
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if(File.Exists(e.FullPath))
            {
                EnqueueEvent(new FileChangedEvent { FullFilePath = e.FullPath, FileName = e.Name });
                return;
            }

            EnqueueEvent(new DirectoryChangedEvent { FullDirectoryPath = e.FullPath, DirectoryName = e.Name });
        }

        public QueuedFileSystemEvent Dequeue()
        {
            QueuedFileSystemEvent fileSystemEvent;

            queuedEvents.TryDequeue(out fileSystemEvent);

            return fileSystemEvent;
        }
    }
}