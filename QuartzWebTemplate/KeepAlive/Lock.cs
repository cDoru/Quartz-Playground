using System;
using System.Threading;

namespace QuartzWebTemplate.KeepAlive
{
    public class Lock : IDisposable
    {
        private readonly object _lockObj;

        public Lock(object lockObj) : this(lockObj, TimeSpan.FromSeconds(5)) { }

        private Lock(object lockObj, TimeSpan timeout)
        {
            _lockObj = lockObj;
            
            if (!Monitor.TryEnter(_lockObj, timeout))
                throw new TimeoutException();
        }

        public void Dispose()
        {
            Monitor.Exit(_lockObj);
        }
    }
}