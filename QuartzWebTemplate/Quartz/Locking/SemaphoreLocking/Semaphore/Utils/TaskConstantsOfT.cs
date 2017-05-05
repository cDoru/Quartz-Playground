using System.Threading.Tasks;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Utils
{
    /// <summary>
    /// Provides completed task constants.
    /// </summary>
    /// <typeparam name="T">The type of the task result.</typeparam>
    public static class TaskConstants<T>
    {
        private static readonly Task<T> DefaultValue = TaskShim.FromResult(default(T));

        private static readonly Task<T> NeverValue = new TaskCompletionSource<T>().Task;

        private static readonly Task<T> CanceledValue = CanceledTask();

        private static Task<T> CanceledTask()
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetCanceled();
            return tcs.Task;
        }

        /// <summary>
        /// A task that has been completed with the default value of <typeparamref name="T"/>.
        /// </summary>
        public static Task<T> Default
        {
            get
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// A <see cref="Task"/> that will never complete.
        /// </summary>
        public static Task<T> Never
        {
            get
            {
                return NeverValue;
            }
        }

        /// <summary>
        /// A task that has been canceled.
        /// </summary>
        public static Task<T> Canceled
        {
            get
            {
                return CanceledValue;
            }
        }
    }
}