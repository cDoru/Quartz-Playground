using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace QuartzWebTemplate.Services
{
    public class FailingHelloService : IFailingHelloService 
    {
        void IFailingHelloService.FailToSayHello()
        {
            throw new FailToSayHelloException();
            //Trace.WriteLine("fail to say hello");
        }
    }

    [Serializable]
    public class FailToSayHelloException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public FailToSayHelloException()
        {
        }

        public FailToSayHelloException(string message) : base(message)
        {
        }

        public FailToSayHelloException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FailToSayHelloException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}