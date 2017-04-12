using System;

namespace QuartzWebTemplate.Quartz.Locking.Contracts
{
    public interface IDisposableShim : IDisposable
    {
        bool AcquisitionFailed { get; }
    }
}