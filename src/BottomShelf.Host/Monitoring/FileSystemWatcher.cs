using System;
using System.Collections.Concurrent;
using System.IO;

namespace BottomShelf.Host.Monitoring
{
    public class FileSystemWatcher
    {
        private readonly string monitoredPath;
        private readonly int fileSystemPoll;
        private System.IO.FileSystemWatcher internalWatcher;
        private readonly ConcurrentDictionary<string, MonitoredDirectory> directories = new ConcurrentDictionary<string, MonitoredDirectory>();

        public FileSystemWatcher(string path, int fileSystemPoll)
        {
            monitoredPath = path;
            this.fileSystemPoll = fileSystemPoll;
        }

        public event EventHandler<CreatedEventArgs> Created;
        public event EventHandler<DeletedEventArgs> Deleted;
        public event EventHandler<RenamedEventArgs> Renamed;
        public event EventHandler<ChangedEventArgs> Changed;

        public void Start()
        {
            internalWatcher = new System.IO.FileSystemWatcher(monitoredPath) {IncludeSubdirectories = true};
            internalWatcher.Created += OnCreated;
            internalWatcher.Changed += OnChanged;
            internalWatcher.Deleted += OnDeleted;
            internalWatcher.Renamed += OnRenamed;
            internalWatcher.EnableRaisingEvents = true;

            foreach(var directoryPath in Directory.GetDirectories(monitoredPath))
                directories.AddOrUpdate(directoryPath, new MonitoredDirectory(this, directoryPath, fileSystemPoll), (p, existing) => existing);
        }

        internal void RaiseCreated(string path)
        {
            Created(this, new CreatedEventArgs(path));
        }

        internal void RaiseChanged(string path)
        {
            Changed(this, new ChangedEventArgs(path));
        }

        internal void RaiseRenamed(string path, string newPath)
        {
            Renamed(this, new RenamedEventArgs(path, newPath));
        }

        internal void RaiseDeleted(string path)
        {
            Deleted(this, new DeletedEventArgs(path));
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if(!Directory.Exists(e.FullPath)) return;

            var rootServiceDirectory = GetRootServiceDirectory(e.FullPath);

            if(!directories.ContainsKey(rootServiceDirectory))
            {
                var directory = directories.AddOrUpdate(rootServiceDirectory, new MonitoredDirectory(this, rootServiceDirectory, fileSystemPoll), (p, existing) => existing);
                directory.Create();
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if(!Directory.Exists(e.FullPath)) return;

            var rootServiceDirectory = GetRootServiceDirectory(e.FullPath);

            directories
                .GetOrAdd(rootServiceDirectory, p => new MonitoredDirectory(this, rootServiceDirectory, fileSystemPoll))
                .Change();
        }

        private void OnRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            var rootServiceDirectory = GetRootServiceDirectory(e.OldFullPath);
            var newRootServiceDirectory = GetRootServiceDirectory(e.FullPath);

            if(rootServiceDirectory == newRootServiceDirectory) return;

            directories
                .GetOrAdd(rootServiceDirectory, p => new MonitoredDirectory(this, rootServiceDirectory, fileSystemPoll))
                .Rename(newRootServiceDirectory);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            var rootServiceDirectory = GetRootServiceDirectory(e.FullPath);

            MonitoredDirectory directory;
            if(directories.TryRemove(rootServiceDirectory, out directory))
                directory.Delete();
        }

        private string GetRootServiceDirectory(string fullPath)
        {
            var segments = fullPath.Replace(monitoredPath, string.Empty).Split(new[] {Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries);

            if(segments.Length == 1)
                return fullPath;

            var rootServiceDirectory = segments[0];

            return Path.Combine(monitoredPath, rootServiceDirectory);
        }
    }
}