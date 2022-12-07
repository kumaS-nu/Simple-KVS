using Newtonsoft.Json;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace kumaS.SimpleKVS.External.JsonDotNet
{
    /// <summary>
    /// <para>Wrapper for Json.net <see cref="JsonConvert"/>.</para>
    /// <para>Json.net <see cref="JsonConvert"/>のラッパー。</para>
    /// </summary>
    public sealed class JsonDotNetSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            var byteData = new byte[data.Length];
            var span = new Span<byte>(byteData);
            data.Read(span);
            var content = Encoding.UTF8.GetString(byteData);
            
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            buffer.Write(Encoding.UTF8.GetBytes(content));
        }

        /// <inheritdoc/>
        public async ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            var byteData = new byte[data.Length];
            var span = new Memory<byte>(byteData);
            await data.ReadAsync(span);
            var content = Encoding.UTF8.GetString(byteData);
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            return buffer.WriteAsync(Encoding.UTF8.GetBytes(content)).AsTask();
        }
    }
}
