using System;

namespace BottomShelf.Host.Monitoring
{
    public class FileChangedEventArgs : EventArgs
    {
        public FileChangedEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}