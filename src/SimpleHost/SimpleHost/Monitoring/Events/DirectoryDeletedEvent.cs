namespace SimpleHost.Monitoring.Events
{
    public class DirectoryDeletedEvent : QueuedFileSystemEvent
    {
        public string FullDirectoryPath { get; set; }
        public string DirectoryName { get; set; }
    }
}