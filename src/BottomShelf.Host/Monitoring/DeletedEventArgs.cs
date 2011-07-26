using System;

namespace BottomShelf.Host.Monitoring
{
    public class DeletedEventArgs : EventArgs
    {
        public string Path { get; private set; }

        public DeletedEventArgs(string path)
        {
            Path = path;
        }
    }
}