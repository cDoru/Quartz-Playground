using System;

namespace QuartzWebTemplate.Quartz.Locking
{
    public interface IAppLock
    {
        LockAcquisitionResult TryAcquire(string lockName, TimeSpan timeout = default(TimeSpan));
        LockReleaseResult ReleaseLock(string lockName, string lockOwner);
        bool VerifyLockOwnership(string lockName, string lockOwner);
    }
}
