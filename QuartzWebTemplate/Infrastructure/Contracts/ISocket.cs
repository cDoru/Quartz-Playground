using System;

namespace QuartzWebTemplate.Infrastructure.Contracts
{
    public interface ISocket
    {
        void Send(string message, ConsoleColor color);
    }
}
