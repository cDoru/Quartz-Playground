using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace ProtobufMessages.Client
{
    public class ProtobufPackageInfo : IPackageInfo
    {
        public ProtobufPackageInfo(DefeatMessage.Types.Type type, DefeatMessage body)
        {
            Type = type;
            Key = type.ToString();
            Body = body;
        }

        public string Key { get; private set; }

        public DefeatMessage Body { get; private set; }
        public DefeatMessage.Types.Type Type { get; private set; }
    }
}
