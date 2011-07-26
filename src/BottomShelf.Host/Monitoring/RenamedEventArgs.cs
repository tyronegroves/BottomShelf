using System;

namespace BottomShelf.Host.Monitoring
{
    public class RenamedEventArgs : EventArgs
    {
        public string OldPath { get; private set; }
        public string Path { get; private set; }

        public RenamedEventArgs(string oldPath, string path)
        {
            OldPath = oldPath;
            Path = path;
        }
    }
}