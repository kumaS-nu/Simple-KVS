using System;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;

using MemoryPack;

namespace kumaS.SimpleKVS.External.MemPack
{
    /// <summary>
    /// <para>Wrapper for <see cref="MemoryPackSerializer"/>.</para>
    /// <para><see cref="MemoryPackSerializer"/>のラッパー。</para>
    /// </summary>
    public sealed class MemPackSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            var d = new byte[data.Length];
            var span = new Span<byte>(d);
            data.Read(span);
            return MemoryPackSerializer.Deserialize<T>(span);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            ArrayBufferWriter<byte> writer = new ArrayBufferWriter<byte>();
            MemoryPackSerializer.Serialize(writer, obj);
            buffer.Write(writer.WrittenSpan);
        }

        /// <inheritdoc/>
        public ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            return MemoryPackSerializer.DeserializeAsync<T>(data);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            return MemoryPackSerializer.SerializeAsync(buffer, obj).AsTask();
        }
    }
}
