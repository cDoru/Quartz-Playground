using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace ProtobufMessages.Server
{
    public class ProtobufAppServer : AppServer<ProtobufAppSession, ProtobufRequestInfo>
    {
        public ProtobufAppServer()
            : base(new DefaultReceiveFilterFactory<ProtobufReceiveFilter, ProtobufRequestInfo>())
        {
        }
    }
}
