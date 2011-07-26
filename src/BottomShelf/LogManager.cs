using System;

namespace BottomShelf
{
    public static class LogManager
    {
        private static Func<Type, ILog> getLog = type => new ConsoleLog(type);

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