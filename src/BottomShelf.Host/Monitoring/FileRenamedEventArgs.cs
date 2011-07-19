using System;

namespace BottomShelf.Host.Monitoring
{
    public class FileRenamedEventArgs : EventArgs
    {
        public FileRenamedEventArgs(string originalFilePath, string filePath)
        {
            OriginalFilePath = originalFilePath;
            FilePath = filePath;
        }

        public string OriginalFilePath { get; private set; }
        public string FilePath { get; private set; }
    }
}