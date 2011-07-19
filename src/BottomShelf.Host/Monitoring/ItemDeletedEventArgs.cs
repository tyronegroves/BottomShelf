using System;

namespace BottomShelf.Host.Monitoring
{
    public class ItemDeletedEventArgs : EventArgs
    {
        public ItemDeletedEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}