using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using QuartzWebTemplate.Quartz;

namespace QuartzWebTemplate.Jobs
{
    public class SpinJob : ISelfDescribingJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public JobInfo Describe
        {
            get { throw new NotImplementedException(); }
        }

        public Action<SimpleScheduleBuilder> Cron
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasActiveSchedule
        {
            get { throw new NotImplementedException(); }
        }
    }
}