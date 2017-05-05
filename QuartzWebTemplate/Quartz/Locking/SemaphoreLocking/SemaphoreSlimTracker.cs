using System;
using System.Threading;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking
{
    /// <summary>
    /// IDisposable tracker of the SemaphoreSlim (a more performant version of the Semaphore)
    /// </summary>
    public sealed class SemaphoreSlimTracker : IDisposable
    {
        /// <summary>
        /// Sync root object
        /// </summary>
        private readonly object _syncRoot;

        /// <summary>
        /// Semaphore to track
        /// </summary>
        private readonly AsyncSemaphore _semaphore;

        private int _refCount;

        public int ReferenceCount
        {
            get
            {
                lock (_syncRoot)
                {
                    return _refCount;
                }
            }
        }

        public SemaphoreSlimTracker()
        {
            _semaphore = new AsyncSemaphore(1);
            _refCount = 1;
            _syncRoot = new object();
        }

        public void Acquire()
        {
            var ct = new CancellationTokenSource(TimeSpan.FromMinutes(3)).Token;
            try
            {
                _semaphore.Wait(ct);
            }
            catch(TaskCanceledException)
            {
                throw new TimeoutException("Possible deadlock.");
            }

        }

        public async Task AcquireAsync()
        {
            var ct = new CancellationTokenSource(TimeSpan.FromMinutes(3)).Token;
            try
            {
                await _semaphore.WaitAsync(ct);
            }
            catch (TaskCanceledException)
            {
                throw new TimeoutException("Possible deadlock.");
            }
        }

        public SemaphoreSlimTracker IncRefCounter()
        {
            lock (_syncRoot)
            {
                _refCount++;
                return this;
            }
        }

        public void Release()
        {
            _semaphore.Release();
            lock (_syncRoot)
            {
                _refCount--;
            }
        }

        // ReSharper disable once EmptyDestructor
        ~SemaphoreSlimTracker()
        {

        }

        public void Dispose()
        {
            //_semaphore.Dispose();
            GC.SuppressFinalize(this);
        }
    }


    ///// <summary>
    ///// IDisposable tracker of the SemaphoreSlim (a more performant version of the Semaphore)
    ///// </summary>
    //public sealed class SemaphoreSlimTracker : IDisposable
    //{
    //    /// <summary>
    //    /// Sync root object
    //    /// </summary>
    //    private readonly object _syncRoot;

    //    /// <summary>
    //    /// Semaphore to track
    //    /// </summary>
    //    private readonly SemaphoreSlim _semaphore;
        
    //    private int _refCount;

    //    public int ReferenceCount
    //    {
    //        get
    //        {
    //            lock (_syncRoot)
    //            {
    //                return _refCount;
    //            }
    //        }
    //    }

    //    public SemaphoreSlimTracker()
    //    {
    //        _semaphore = new SemaphoreSlim(1);
    //        _refCount = 1;
    //        _syncRoot = new object();
    //    }

    //    public void Acquire()
    //    {
    //        if (!_semaphore.Wait(TimeSpan.FromMinutes(3)))
    //        {
    //            throw new TimeoutException("Possible deadlock.");
    //        }
    //    }

    //    public async Task AcquireAsync()
    //    {
    //        if (!(await _semaphore.WaitAsync(TimeSpan.FromMinutes(3))))
    //        {
    //            throw new TimeoutException("Possible deadlock.");
    //        }
    //    }

    //    public SemaphoreSlimTracker IncRefCounter()
    //    {
    //        lock (_syncRoot)
    //        {
    //            _refCount++;
    //            return this;
    //        }
    //    }

    //    public void Release()
    //    {
    //        _semaphore.Release();
    //        lock (_syncRoot)
    //        {
    //            _refCount--;
    //        }
    //    }

    //    // ReSharper disable once EmptyDestructor
    //    ~SemaphoreSlimTracker()
    //    {
            
    //    }

    //    public void Dispose()
    //    {
    //        _semaphore.Dispose();
    //        GC.SuppressFinalize(this);
    //    }
    //}
}