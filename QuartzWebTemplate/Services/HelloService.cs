using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace QuartzWebTemplate.Services
{
    public interface IHelloService
    {
        void SayHello();
    }

    public interface IFailingHelloService
    {
        void FailToSayHello();
    }


    public class HelloService : IHelloService
    {
        void IHelloService.SayHello()
        {
            //Console.WriteLine("hello");
            Trace.WriteLine("well hello");
        }
    }
}