using System;

namespace BottomShelf.Host.Monitoring
{
    public class CreatedEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public CreatedEventArgs(string path)
        {
            Path = path;
        }
    }
}