namespace SimpleHost.Monitoring.Events
{
    public class FileRenamedEvent : QueuedFileSystemEvent
    {
        public string FullFilePath { get; set; }
        public string OldFilePath { get; set; }
        public string FileName { get; set; }
        public string OldFileName { get; set; }
    }
}