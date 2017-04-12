using System;

namespace QuartzWebTemplate.Quartz.Locking
{
    public interface IDisposableShim : IDisposable
    {
        bool AcquisitionFailed { get; }
    }
}