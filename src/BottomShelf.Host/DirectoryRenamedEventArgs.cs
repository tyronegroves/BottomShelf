using System;

namespace BottomShelf.Host
{
    public class DirectoryRenamedEventArgs : EventArgs
    {
        public DirectoryRenamedEventArgs(string originalPath, string  path)
        {
            OriginalPath = originalPath;
            Path = path;
        }

        public string OriginalPath { get; private set; }
        public string Path { get; private set; }
    }
}