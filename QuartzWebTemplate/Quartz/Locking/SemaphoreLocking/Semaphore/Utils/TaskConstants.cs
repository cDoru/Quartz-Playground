using System.Threading.Tasks;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Utils
{
    /// <summary>
    /// Provides completed task constants.
    /// </summary>
    public static class TaskConstants
    {
        private static readonly Task<bool> booleanTrue = TaskShim.FromResult(true);
        private static readonly Task<int> IntNegativeOne = TaskShim.FromResult(-1);

        /// <summary>
        /// A task that has been completed with the value <c>true</c>.
        /// </summary>
        public static Task<bool> BooleanTrue
        {
            get
            {
                return booleanTrue;
            }
        }

        /// <summary>
        /// A task that has been completed with the value <c>false</c>.
        /// </summary>
        public static Task<bool> BooleanFalse
        {
            get
            {
                return TaskConstants<bool>.Default;
            }
        }

        /// <summary>
        /// A task that has been completed with the value <c>0</c>.
        /// </summary>
        public static Task<int> Int32Zero
        {
            get
            {
                return TaskConstants<int>.Default;
            }
        }

        /// <summary>
        /// A task that has been completed with the value <c>-1</c>.
        /// </summary>
        public static Task<int> Int32NegativeOne
        {
            get
            {
                return IntNegativeOne;
            }
        }

        /// <summary>
        /// A <see cref="Task"/> that has been completed.
        /// </summary>
        public static Task Completed
        {
            get
            {
                return booleanTrue;
            }
        }

        /// <summary>
        /// A <see cref="Task"/> that will never complete.
        /// </summary>
        public static Task Never
        {
            get
            {
                return TaskConstants<bool>.Never;
            }
        }

        /// <summary>
        /// A task that has been canceled.
        /// </summary>
        public static Task Canceled
        {
            get
            {
                return TaskConstants<bool>.Canceled;
            }
        }
    }
}