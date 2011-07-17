using System;

namespace BottomShelf
{
    public interface ILog
    {
        void Info(string format, params object[] arguments);
        void Warn(string format, params object[] arguments);
        void Error(Exception exception);
    }
}