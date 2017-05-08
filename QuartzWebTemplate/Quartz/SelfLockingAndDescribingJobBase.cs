using System;
using Quartz;
using QuartzWebTemplate.Exceptions;
using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz
{
    public abstract class SelfLockingAndDescribingJobBase : SelfDescribingJobBase
    {
        private const string DataMapKey = "job-identifier";
        private readonly ILock _lock;

        protected SelfLockingAndDescribingJobBase(ILock @lock)
        {
            _lock = @lock;
        }

        protected override void ExecuteInner(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            if (!dataMap.ContainsKey(DataMapKey))
            {
                throw new DataMapKeyMissingException(string.Format("Unique job identifier key missing in mergeddatamap. Please include it  with a unique identifier/id assigned. E.g. {0}:12345", DataMapKey));
            }

            string identifier;
            if ((identifier = dataMap[DataMapKey] as string) == null)
            {
                throw new DataMapIdentifierNullException(string.Format("Key {0} not present in mergeddatamap. Please include this key with a unique identifier/id assigned", DataMapKey));
            }

            using (var handle = _lock.Acquire(GetLockHeader + identifier, TimeSpan.FromSeconds(5)))
            {
                if (handle.AcquisitionFailed)
                {
                    throw new LockAcquisitionException(string.Format("Lock {0} could not be acquired. There was another operation using the same lock currently running", GetLockHeader + identifier));
                }

                // do your shit
                ExecuteInnerInner(context);
            }
        }

        /// <summary>
        /// Not good. leaving this here to improve it at a later time. afterall this is a learning playground, so only parts of this will have production reliability
        /// </summary>
        /// <param name="context"></param>
        protected abstract void ExecuteInnerInner(IJobExecutionContext context);
        protected virtual string GetLockHeader { get { return GetType().Name + "-"; } }
    }
}