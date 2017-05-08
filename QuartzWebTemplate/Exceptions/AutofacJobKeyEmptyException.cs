using System;
using System.Runtime.Serialization;

namespace QuartzWebTemplate.Exceptions
{
    [Serializable]
    public class AutofacJobKeyEmptyException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public AutofacJobKeyEmptyException()
        {
        }

        public AutofacJobKeyEmptyException(string message)
            : base(message)
        {
        }

        public AutofacJobKeyEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AutofacJobKeyEmptyException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}