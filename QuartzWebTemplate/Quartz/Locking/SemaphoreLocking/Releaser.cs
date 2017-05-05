using System;
using System.Collections.Concurrent;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking
{
    public sealed class Releaser : IDisposable
    {
        /// <summary>
        /// The sync object
        /// </summary>
        private readonly object _token;

        /// <summary>
        /// The slim semaphores trackers 
        /// </summary>
        private readonly ConcurrentDictionary<object, SemaphoreSlimTracker> _semaphores;

        public Releaser(ConcurrentDictionary<object, SemaphoreSlimTracker> semaphores, object token)
        {
            _token = token;
            _semaphores = semaphores;
        }

        // ReSharper disable once EmptyDestructor
        ~Releaser()
        {
        }

        public void Dispose()
        {
            Release(_semaphores, _token);

            GC.SuppressFinalize(this);
        }

        private static void Release(ConcurrentDictionary<object, SemaphoreSlimTracker> semaphores, object token)
        {
            SemaphoreSlimTracker tracker;
            
            if (!semaphores.TryGetValue(token, out tracker))
            {
                return;
            }

            tracker.Release();
            
            if (tracker.ReferenceCount != 0)
            {
                return;
            }
                
            lock (tracker)
            {
                SemaphoreSlimTracker tmp;
                if (tracker.ReferenceCount == 0 && semaphores.TryRemove(token, out tmp))
                    tracker.Dispose();
            }
        }
    }
}