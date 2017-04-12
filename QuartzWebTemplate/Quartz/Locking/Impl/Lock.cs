using System;
using QuartzWebTemplate.Exceptions;
using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public class Lock : ILock
    {
        private readonly IAppLock _appLock;
        public Lock(IAppLock appLock)
        {
            _appLock = appLock;
        }

        public IDisposableShim Acquire(string lockName, TimeSpan timeout = new TimeSpan())
        {
            var result = _appLock.TryAcquire(lockName, timeout);

            return !result.Success ? new DisposableShim(true) : new DisposableShim(_appLock, result.LockOwner, lockName);
        }

        public IDisposableShim AcquireWithFail(string lockName, TimeSpan timeout = new TimeSpan())
        {
            var result = _appLock.TryAcquire(lockName, timeout);

            if (!result.Success)
            {
                throw new LockAcquisitionException(string.Format("Failed to acquire lock {0}", lockName));
            }

            return new DisposableShim(_appLock, result.LockOwner, lockName);
        }

        private class DisposableShim : IDisposableShim
        {
            private readonly IAppLock _appLock;
            private readonly string _owner;
            private readonly string _lockName;

            public bool AcquisitionFailed { get; private set; }

            internal DisposableShim(IAppLock
                appLock, string owner, string lockName)
            {
                _appLock = appLock;
                _owner = owner;
                _lockName = lockName;
                AcquisitionFailed = false;
            }

            internal DisposableShim(bool failed)
            {
                AcquisitionFailed = failed;
                _appLock = null;
                _owner = null;
                _lockName = null;
            }

            public void Dispose()
            {
                if (!AcquisitionFailed)
                {
                    _appLock.ReleaseLock(_lockName, _owner);
                }
            }
        }
    }
}