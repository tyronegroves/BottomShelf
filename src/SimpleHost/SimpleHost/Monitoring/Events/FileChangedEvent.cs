namespace SimpleHost.Monitoring.Events
{
    public class FileChangedEvent : QueuedFileSystemEvent
    {
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
    }
}