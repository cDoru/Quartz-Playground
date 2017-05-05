using System;
using System.Runtime.Serialization;

namespace QuartzWebTemplate.Quartz.Locking.SemaphoreLocking.Semaphore.Exceptions
{
    [Serializable]
    public class RethrowException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RethrowException()
        {
        }

        public RethrowException(string message) : base(message)
        {
        }

        public RethrowException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RethrowException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}