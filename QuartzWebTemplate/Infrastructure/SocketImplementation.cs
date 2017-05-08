using System;
using System.IO;
using System.Net;
using Google.ProtocolBuffers;
using ProtobufMessages.Client;
using QuartzWebTemplate.Infrastructure.Contracts;
using SuperSocket.ClientEngine;

namespace QuartzWebTemplate.Infrastructure
{
    public class SocketImplementation : ISocket
    {
        private readonly EasyClient _socketClient;
        public SocketImplementation()
        {
            _socketClient = new EasyClient();
            InitializeServer();
        }

        public void Send(string message, ConsoleColor color)
        {
            var flag = _socketClient.ConnectAsync(new DnsEndPoint("127.0.0.1", 2012));
            if (flag.Result)
            {
                var consoleColor = ConsoleColor.DarkRed.ToString();
                var callMessage = CallMessage.CreateBuilder()
                    .SetContent(string.Format("{0}%{1}", message, consoleColor)).Build();
                var msg = DefeatMessage.CreateBuilder()
                    .SetType(DefeatMessage.Types.Type.CallMessage)
                    .SetCallMessage(callMessage).Build();

                using (var stream = new MemoryStream())
                {
                    var os = CodedOutputStream.CreateInstance(stream);
                    os.WriteMessageNoTag(msg);
                    os.Flush();
                    var data = stream.ToArray();
                    _socketClient.Send(new ArraySegment<byte>(data));
                }
            }
        }

        private static void InitializeServer()
        {
            var client = new EasyClient();
            client.Initialize(new ProtobufReceiveFilter(), (info =>
            {
                switch (info.Type)
                {
                    case DefeatMessage.Types.Type.BackMessage:
                        Console.WriteLine("BackMessage:{0}", info.Body.BackMessage.Content);
                        break;
                    case DefeatMessage.Types.Type.CallMessage:
                        Console.WriteLine("CallMessage:{0}", info.Body.CallMessage.Content);
                        break;
                }
            }));
        }
    }
}