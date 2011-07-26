using System;

namespace BottomShelf.Host.Monitoring
{
    public class ChangedEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public ChangedEventArgs(string path)
        {
            Path = path;
        }
    }
}