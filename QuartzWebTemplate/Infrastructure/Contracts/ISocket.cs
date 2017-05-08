using System;

namespace QuartzWebTemplate.Infrastructure.Contracts
{
    interface ISocket
    {
        void Send(string message, ConsoleColor color);
    }
}
