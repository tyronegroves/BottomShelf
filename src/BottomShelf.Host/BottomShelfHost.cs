using System;
using BottomShelf.Host.Monitoring;

namespace BottomShelf.Host
{
    public class BottomShelfHost
    {
        private readonly int fileSystemPoll;
        private FileSystemWatcher watcher;

        public BottomShelfHost(int fileSystemPoll)
        {
            this.fileSystemPoll = fileSystemPoll;
        }

        public void Start(string[] arguments)
        {
            watcher = new FileSystemWatcher(@".\Services", fileSystemPoll);
            watcher.Changed += watcher_Changed;
            watcher.Created += watcher_Created;
            watcher.Deleted += watcher_Deleted;
            watcher.Renamed += watcher_Renamed;
            watcher.Start();
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("Renamed '{0}' to '{1}'", e.OldPath, e.Path);
        }

        private void watcher_Deleted(object sender, DeletedEventArgs e)
        {
            Console.WriteLine("Deleted '{0}'", e.Path);
        }

        private void watcher_Created(object sender, CreatedEventArgs e)
        {
            Console.WriteLine("Created '{0}'", e.Path);
        }

        private void watcher_Changed(object sender, ChangedEventArgs e)
        {
            Console.WriteLine("Changed '{0}'", e.Path);
        }

        public void Stop()
        {
        }
    }
}