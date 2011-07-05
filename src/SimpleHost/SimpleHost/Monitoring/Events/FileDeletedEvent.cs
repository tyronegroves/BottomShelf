namespace SimpleHost.Monitoring.Events
{
    public class FileDeletedEvent : QueuedFileSystemEvent
    {
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
    }
}