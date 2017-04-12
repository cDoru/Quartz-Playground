using System;

namespace QuartzWebTemplate.Quartz.Locking.Helpers
{
    internal static class DistributedLockHelpers
    {
        public static int ToInt32Timeout(this TimeSpan timeout, string paramName = null)
        {
            // based on http://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Task.cs,959427ac16fa52fa

            var totalMilliseconds = (long)timeout.TotalMilliseconds;
            if (totalMilliseconds < -1 || totalMilliseconds > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(paramName ?? "timeout");
            }

            return (int)totalMilliseconds;
        }
    }
}