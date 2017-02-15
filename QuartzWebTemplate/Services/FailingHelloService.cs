using System;

namespace QuartzWebTemplate.Services
{
    public class FailingHelloService : IFailingHelloService 
    {
        void IFailingHelloService.FailToSayHello()
        {
            throw new Exception();
        }
    }
}