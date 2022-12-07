using System.IO;
using System.Threading.Tasks;

using MessagePack;

namespace kumaS.SimpleKVS.External.MsgPack
{
    /// <summary>
    /// <para>Wrapper for <see cref="MessagePackSerializer"/>.</para>
    /// <para><see cref="MessagePackSerializer"/>のラッパー。</para>
    /// </summary>
    public sealed class MsgPackSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            return MessagePackSerializer.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            MessagePackSerializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            return MessagePackSerializer.DeserializeAsync<T>(data);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            return MessagePackSerializer.SerializeAsync(buffer, obj);
        }
    }
}
