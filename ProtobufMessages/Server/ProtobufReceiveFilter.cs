using System;
using Google.ProtocolBuffers;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace ProtobufMessages.Server
{
    /// <summary>
    ///     A decoder that splits the received {@link ByteBuf}s dynamically by the
    ///     value of the Google Protocol Buffers
    ///     <a href="http://code.google.com/apis/protocolbuffers/docs/encoding.html#varints">
    ///         Base
    ///         128 Varints
    ///     </a>
    ///     integer length field in the message.For example:
    ///    
    ///         BEFORE DECODE (302 bytes)       AFTER DECODE (300 bytes)
    ///         +--------+---------------+      +---------------+
    ///         | Length | Protobuf Data |----->| Protobuf Data |
    ///         | 0xAC02 |  (300 bytes)  |      |  (300 bytes)  |
    ///         +--------+---------------+      +---------------+
    /// </summary>
    public class ProtobufReceiveFilter : IReceiveFilter<ProtobufRequestInfo>, IOffsetAdapter, IReceiveFilterInitializer
    {
        private int 
            _mParsedLength;

        private int _mOrigOffset;

        void IReceiveFilterInitializer.Initialize(IAppServer appServer, IAppSession session)
        {
            _mOrigOffset = session.SocketSession.OrigReceiveOffset;
        }

        public ProtobufRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;
            var readOffset = offset - m_OffsetDelta;

            var stream = CodedInputStream.CreateInstance(readBuffer, readOffset, length);
            var varint32 = (int)stream.ReadRawVarint32();
            if (varint32 <= 0) return null;

            var headLen = (int)stream.Position - readOffset;
            rest = length - varint32 - headLen + _mParsedLength;

            if (rest >= 0)
            {
                var body = stream.ReadRawBytes(varint32);
                var message = DefeatMessage.ParseFrom(body);
                var requestInfo = new ProtobufRequestInfo(message.Type, message);
                InternalReset();
                return requestInfo;
            }
            
            _mParsedLength += length;
            m_OffsetDelta = _mParsedLength;
            rest = 0;

            var expectedOffset = offset + length;
            var newOffset = _mOrigOffset + m_OffsetDelta;

            if (newOffset < expectedOffset)
            {
                Buffer.BlockCopy(readBuffer, offset - _mParsedLength + length, readBuffer, _mOrigOffset, _mParsedLength);
            }

            return null;
        }



        private int m_OffsetDelta;

        /// <summary>
        /// Gets the offset delta.
        /// </summary>
        int IOffsetAdapter.OffsetDelta
        {
            get { return m_OffsetDelta; }
        }

        private void InternalReset()
        {
            _mParsedLength = 0;
            m_OffsetDelta = 0;
        }

        public void Reset()
        {
            InternalReset();
        }

        public int LeftBufferSize
        {
            get { return _mParsedLength; }
        }

        public IReceiveFilter<ProtobufRequestInfo> NextReceiveFilter
        {
            get { return null; }
        }

        public FilterState State
        {
            get;
            // ReSharper disable once UnusedAutoPropertyAccessor.Global
            protected set;
        }
    }
}