using System.IO;
using System.Threading.Tasks;

using MessagePack;

namespace kumaS.SimpleKVS.External.MsgPack
{
    /// <summary>
    /// <para>Wrapper for <see cref="MessagePackSerializer"/> with LZ4 compress option.</para>
    /// <para>LZ4圧縮オプション付きの<see cref="MessagePackSerializer"/>のラッパー。</para>
    /// </summary>
    public sealed class MsgPackLZ4Serializer : ISerializer
    {
        private static readonly MessagePackSerializerOptions option = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray).WithResolver(MessagePack.Resolvers.StaticCompositeResolver.Instance);

        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            return MessagePackSerializer.Deserialize<T>(data, option);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            MessagePackSerializer.Serialize(buffer, obj, option);
        }

        /// <inheritdoc/>
        public ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            return MessagePackSerializer.DeserializeAsync<T>(data, option);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            return MessagePackSerializer.SerializeAsync(buffer, obj, option);
        }
    }
}
