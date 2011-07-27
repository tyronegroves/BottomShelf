using System;
using System.Threading;

namespace BottomShelf
{
    [Serializable]
    public class ConsoleLog : ILog
    {
        private readonly Type type;

        public ConsoleLog(Type type)
        {
            this.type = type;
        }

        public void Info(string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            Console.WriteLine("{0:yyyy-MM-dd hh:mm:ss.fff} [{1}] INFO {2} - {3}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, type, message);
        }

        public void Warn(string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            Console.WriteLine("{0:yyyy-MM-dd hh:mm:ss.fff} [{1}] WARN {2} - {3}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, type, message);
        }

        public void Error(Exception exception)
        {
            Console.WriteLine("{0:yyyy-MM-dd hh:mm:ss.fff} [{1}] ERROR {2} - {3}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, type, exception);
        }
    }
}