using System;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Extensions;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Queue
{
    public sealed class CompleteDisposable<T> : IDisposable
    {
        private readonly TaskCompletionSource<T>[] _taskCompletionSources;
        private readonly T _result;

        public CompleteDisposable(T result, params TaskCompletionSource<T>[] taskCompletionSources)
        {
            _result = result;
            _taskCompletionSources = taskCompletionSources;
        }

        public void Dispose()
        {
            foreach (var cts in _taskCompletionSources)
                cts.TrySetResultWithBackgroundContinuations(_result);
        }
    }
}