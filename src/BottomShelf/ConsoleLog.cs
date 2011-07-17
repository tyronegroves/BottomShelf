using System;

namespace BottomShelf
{
    public class ConsoleLog : ILog
    {
        public void Info(string format, params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }

        public void Warn(string format, params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }

        public void Error(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}