using System;
using QuartzWebTemplate.Infrastructure.Contracts;

namespace QuartzWebTemplate.Infrastructure
{
    public class NowImplementation : INow
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}