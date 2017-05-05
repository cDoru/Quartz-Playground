using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking
{
    /// <summary>
    /// Critical region helper
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CriticalRegionAsyncLock
    {
        private static readonly ConcurrentDictionary<object, SemaphoreSlimTracker> Semaphores = new ConcurrentDictionary<object, SemaphoreSlimTracker>();
        private readonly ConcurrentDictionary<object, SemaphoreSlimTracker> _semaphores;

        public static async Task<IDisposable> CreateAsync(object syncRoot)
        {
            return await CreateAsync(Semaphores, syncRoot);
        }

        public static IDisposable Create(object syncRoot)
        {
            return Create(Semaphores, syncRoot);
        }

        public CriticalRegionAsyncLock()
        {
            _semaphores = new ConcurrentDictionary<object, SemaphoreSlimTracker>();
        }

        public IDisposable Acquire(object syncRoot)
        {
            return Create(_semaphores, syncRoot);
        }

        public async Task<IDisposable> AcquireAsync(object syncRoot)
        {
            return await CreateAsync(_semaphores, syncRoot);
        }

        private static async Task<IDisposable> CreateAsync(ConcurrentDictionary<object, SemaphoreSlimTracker> semaphores, object syncRoot)
        {
            var tracker = EnsureInstanceLock(semaphores, syncRoot);
            var releaser = new Releaser(semaphores, syncRoot);
            await tracker.AcquireAsync();
            return releaser;
        }

        private static IDisposable Create(ConcurrentDictionary<object, SemaphoreSlimTracker> semaphores, object syncRoot)
        {
            var tracker = EnsureInstanceLock(semaphores, syncRoot);
            var releaser = new Releaser(semaphores, syncRoot);
            tracker.Acquire();
            return releaser;
        }

        private static SemaphoreSlimTracker EnsureInstanceLock(ConcurrentDictionary<object, SemaphoreSlimTracker> semaphores, object syncRoot)
        {
            SemaphoreSlimTracker tracker = null;
            semaphores.AddOrUpdate(syncRoot,
                _ => tracker = new SemaphoreSlimTracker(),
                (_, oldVal) => tracker = oldVal.IncRefCounter());

            return tracker;
        }
    }
}