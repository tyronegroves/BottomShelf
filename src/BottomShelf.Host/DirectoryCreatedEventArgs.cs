using System;

namespace BottomShelf.Host
{
    public class DirectoryCreatedEventArgs : EventArgs
    {
        public DirectoryCreatedEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}