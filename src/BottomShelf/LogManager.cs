using System;

namespace BottomShelf
{
    public static class LogManager
    {
        private static readonly ConsoleLog DefaultLog = new ConsoleLog();
        private static Func<Type, ILog> getLog = type => DefaultLog;

        public static Func<Type, ILog> GetLogFactoryMethod()
        {
            return getLog;
        }

        public static void SetLogFactoryMethod(Func<Type, ILog> getLogMethod)
        {
            getLog = getLogMethod;
        }

        public static ILog GetLog(Type type)
        {
            return getLog(type);
        }

        public static ILog GetLog<T>()
        {
            return getLog(typeof(T));
        }
    }
}