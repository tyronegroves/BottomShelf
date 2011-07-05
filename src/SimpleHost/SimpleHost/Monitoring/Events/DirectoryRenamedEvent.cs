namespace SimpleHost.Monitoring.Events
{
    public class DirectoryRenamedEvent : QueuedFileSystemEvent
    {
        public string FullDirectoryPath { get; set; }
        public string OldDirectoryPath { get; set; }
        public string DirectoryName { get; set; }
        public string OldDirectoryName { get; set; }
    }
}