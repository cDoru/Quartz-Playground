using System;

namespace QuartzWebTemplate.AutoFacConfiguration
{
    /// <summary>
    ///     Predicate to filter jobs to be registered.
    /// </summary>
    /// <param name="jobType">Job type class.</param>
    /// <returns><c>true</c> if job should be registered, <c>false</c> otherwise.</returns>
    public delegate bool JobRegistrationFilter(Type jobType);
}