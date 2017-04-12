using System;
using QuartzWebTemplate.Quartz.Util;

namespace QuartzWebTemplate.Quartz.Entities
{
    public class ApplicationLock
    {
        public Guid Id { get; private set; }

        public DateTime UtcTimestamp { get; set; }

        public string LockName { get; set; }

        public ApplicationLock()
        {
            Id = SequentialGuid.NewSequentialGuid();
        }
    }
}