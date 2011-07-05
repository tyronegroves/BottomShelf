namespace SimpleHost.Monitoring.Events
{
    public class FileCreatedEvent : QueuedFileSystemEvent
    {
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
    }
}