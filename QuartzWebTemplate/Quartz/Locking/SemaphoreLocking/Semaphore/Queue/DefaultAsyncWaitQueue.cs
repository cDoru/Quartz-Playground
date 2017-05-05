using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Contract;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Queue
{
    /// <summary>
    /// The default wait queue implementation, which uses a double-ended queue.
    /// </summary>
    /// <typeparam name="T">The type of the results. If this isn't needed, use <see cref="Object"/>.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DefaultAsyncWaitQueue<>.DebugView))]
    public sealed class DefaultAsyncWaitQueue<T> : IAsyncWaitQueue<T>
    {
        private readonly Deque<TaskCompletionSource<T>> _queue = new Deque<TaskCompletionSource<T>>();

        private int Count
        {
            get { lock (_queue) { return _queue.Count; } }
        }

        bool IAsyncWaitQueue<T>.IsEmpty
        {
            get { return Count == 0; }
        }

        Task<T> IAsyncWaitQueue<T>.Enqueue()
        {
            var tcs = new TaskCompletionSource<T>();
            lock (_queue)
                _queue.AddToBack(tcs);
            return tcs.Task;
        }

        IDisposable IAsyncWaitQueue<T>.Dequeue(T result)
        {
            TaskCompletionSource<T> tcs;
            lock (_queue)
                tcs = _queue.RemoveFromFront();
            return new CompleteDisposable<T>(result, tcs);
        }

        IDisposable IAsyncWaitQueue<T>.DequeueAll(T result)
        {
            TaskCompletionSource<T>[] taskCompletionSources;
            lock (_queue)
            {
                taskCompletionSources = _queue.ToArray();
                _queue.Clear();
            }
            return new CompleteDisposable<T>(result, taskCompletionSources);
        }

        IDisposable IAsyncWaitQueue<T>.TryCancel(Task task)
        {
            TaskCompletionSource<T> tcs = null;
            lock (_queue)
            {
                for (var i = 0; i != _queue.Count; ++i)
                {
                    if (_queue[i].Task != task)
                    {
                        continue;
                    }
                    
                    tcs = _queue[i];
                    _queue.RemoveAt(i);
                    break;
                }
            }

            return tcs == null ? new CancelDisposable<T>() : new CancelDisposable<T>(tcs);
        }

        IDisposable IAsyncWaitQueue<T>.CancelAll()
        {
            TaskCompletionSource<T>[] taskCompletionSources;
            lock (_queue)
            {
                taskCompletionSources = _queue.ToArray();
                _queue.Clear();
            }
            return new CancelDisposable<T>(taskCompletionSources);
        }

        [DebuggerNonUserCode]
        // ReSharper disable once MemberCanBePrivate.Global
        internal sealed class DebugView
        {
            private readonly DefaultAsyncWaitQueue<T> _queue;

            public DebugView(DefaultAsyncWaitQueue<T> queue)
            {
                _queue = queue;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public Task<T>[] Tasks
            {
                get { return _queue._queue.Select(x => x.Task).ToArray(); }
            }
        }
    }
}