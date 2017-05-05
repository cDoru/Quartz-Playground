using System;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking
{
    /// <summary>
    ///  This holds the synchronization object which is locked on by the critical regions
    /// </summary>
    public static class TokenHolder
    {
        /// <summary>
        /// Sync object
        /// </summary>
        public static readonly object SynchronizationToken = new Object();
    }
}
