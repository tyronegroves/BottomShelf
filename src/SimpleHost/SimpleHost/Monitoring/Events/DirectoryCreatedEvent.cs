namespace SimpleHost.Monitoring.Events
{
    public class DirectoryCreatedEvent : QueuedFileSystemEvent
    {
        public string FullDirectoryPath { get; set; }
        public string DirectoryName { get; set; }
    }
}