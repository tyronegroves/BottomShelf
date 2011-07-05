namespace SimpleHost.Monitoring.Events
{
    public class DirectoryChangedEvent : QueuedFileSystemEvent
    {
        public string FullDirectoryPath { get; set; }
        public string DirectoryName { get; set; }
    }
}