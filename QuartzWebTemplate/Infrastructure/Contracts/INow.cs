using System;

namespace QuartzWebTemplate.Infrastructure.Contracts
{
    public interface INow
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}