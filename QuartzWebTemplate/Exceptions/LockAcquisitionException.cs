using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace QuartzWebTemplate.Exceptions
{
    [Serializable]
    public class LockAcquisitionException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public LockAcquisitionException()
        {
        }

        public LockAcquisitionException(string message)
            : base(message)
        {
        }

        public LockAcquisitionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected LockAcquisitionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}