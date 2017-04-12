namespace QuartzWebTemplate.Quartz.Locking
{
    public class LockAcquisitionResult
    {
        public string LockOwner { get; set; }
        public bool Success { get; set; }

        public static LockAcquisitionResult Fail
        {
            get
            {
                return new LockAcquisitionResult
                {
                    LockOwner = string.Empty,
                    Success = false
                };
            }
        }
    }
}