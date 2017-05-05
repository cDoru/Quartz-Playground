using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public class SynchronizationTokenHolder : ISynchronizationTokenHolder
    {
        private readonly object _token;
        public SynchronizationTokenHolder()
        {
            _token = new object();
        }

        public object Token
        {
            get { return _token; }
        }
    }
}