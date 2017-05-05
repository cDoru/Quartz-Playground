using System;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Extensions;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Queue
{
    public sealed class CancelDisposable<T> : IDisposable
    {
        private readonly TaskCompletionSource<T>[] _taskCompletionSources;

        public CancelDisposable(params TaskCompletionSource<T>[] taskCompletionSources)
        {
            _taskCompletionSources = taskCompletionSources;
        }

        public void Dispose()
        {
            foreach (var cts in _taskCompletionSources)
                cts.TrySetCanceledWithBackgroundContinuations();
        }
    }
}