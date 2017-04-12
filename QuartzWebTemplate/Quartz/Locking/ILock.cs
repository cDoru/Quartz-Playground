using System;

namespace QuartzWebTemplate.Quartz.Locking
{
    public interface ILock
    {
        IDisposableShim Acquire(string lockName, TimeSpan timeout = default(TimeSpan));
        IDisposableShim AcquireWithFail(string lockName, TimeSpan timeout = default(TimeSpan));
    }
}