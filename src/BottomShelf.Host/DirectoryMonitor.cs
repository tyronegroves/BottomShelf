using System;
using System.IO;

namespace BottomShelf.Host
{
    public class DirectoryMonitor
    {
        private FileSystemWatcher watcher;

        public void Start(string directory)
        {
            watcher = new FileSystemWatcher(directory) {IncludeSubdirectories = true};
            watcher.Created += (sender, e) =>
                                   {
                                       if(File.Exists(e.FullPath))
                                           FileCreated(this, new FileCreatedEventArgs(e.FullPath, Path.GetDirectoryName(e.FullPath)));
                                       else
                                           DirectoryCreated(this, new DirectoryCreatedEventArgs(e.FullPath));
                                   };

            watcher.Renamed += (sender, e) => DirectoryRenamed(this, new DirectoryRenamedEventArgs(e.OldFullPath, e.FullPath));
            watcher.Deleted += (sender, e) => ItemDeleted(this, new ItemDeletedEventArgs(e.FullPath));

            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            if(watcher != null)
                watcher.EnableRaisingEvents = false;
        }

        public event EventHandler<DirectoryCreatedEventArgs> DirectoryCreated;
        public event EventHandler<DirectoryRenamedEventArgs> DirectoryRenamed;
        public event EventHandler<FileCreatedEventArgs> FileCreated;
        public event EventHandler<ItemDeletedEventArgs> ItemDeleted;
    }
}