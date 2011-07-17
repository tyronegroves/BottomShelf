using System;

namespace BottomShelf.Host.Specs.Mocks
{
    public class TestLogA : ILog
    {
        public void Info(string format, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void Warn(string format, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}