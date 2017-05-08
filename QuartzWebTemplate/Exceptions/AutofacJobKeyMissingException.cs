using System;
using System.Runtime.Serialization;

namespace QuartzWebTemplate.Exceptions
{
    [Serializable]
    public class AutofacJobKeyMissingException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public AutofacJobKeyMissingException()
        {
        }

        public AutofacJobKeyMissingException(string message)
            : base(message)
        {
        }

        public AutofacJobKeyMissingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AutofacJobKeyMissingException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}