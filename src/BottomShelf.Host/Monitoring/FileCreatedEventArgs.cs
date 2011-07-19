using System;

namespace BottomShelf.Host.Monitoring
{
    public class FileCreatedEventArgs : EventArgs
    {
        public FileCreatedEventArgs(string filePath, string directoryPath)
        {
            DirectoryPath = directoryPath;
            FilePath = filePath;
        }

        public string DirectoryPath { get; private set; }
        public string FilePath { get; private set; }
    }
}