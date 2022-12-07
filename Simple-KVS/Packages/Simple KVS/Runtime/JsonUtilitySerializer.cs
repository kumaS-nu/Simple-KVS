using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Wrapper for <see cref="JsonUtility"/>.</para>
    /// <para><see cref="JsonUtility"/>のラッパー。</para>
    /// </summary>
    public class JsonUtilitySerializer : ISerializer, ISerializer4UnityObject
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            var byteData = new byte[data.Length];
            var span = new Span<byte>(byteData);
            data.Read(span);
            var content = Encoding.UTF8.GetString(byteData);
            return JsonUtility.FromJson<T>(content);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            var content = JsonUtility.ToJson(obj);
            buffer.Write(Encoding.UTF8.GetBytes(content));
        }

        /// <inheritdoc/>
        public async ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            var byteData = new byte[data.Length];
            var span = new Memory<byte>(byteData);
            await data.ReadAsync(span);
            var content = Encoding.UTF8.GetString(byteData);
            return JsonUtility.FromJson<T>(content);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            var content = JsonUtility.ToJson(obj);
            return buffer.WriteAsync(Encoding.UTF8.GetBytes(content)).AsTask();
        }

        /// <inheritdoc/>
        public void Deserialize4UnityObject<T>(Stream data, T obj) where T : UnityEngine.Object
        {
            var byteData = new byte[data.Length];
            var span = new Span<byte>(byteData);
            data.Read(span);
            var content = Encoding.UTF8.GetString(byteData);
            JsonUtility.FromJsonOverwrite(content, obj);
        }

        /// <inheritdoc/>
        public void Serialize4UnityObject<T>(Stream buffer, T obj) where T : UnityEngine.Object
        {
            var content = JsonUtility.ToJson(obj);
            buffer.Write(Encoding.UTF8.GetBytes(content));
        }

        /// <inheritdoc/>
        public async ValueTask Deserialize4UnityObjectAsync<T>(Stream data, T value) where T : UnityEngine.Object
        {
            var byteData = new byte[data.Length];
            var span = new Memory<byte>(byteData);
            await data.ReadAsync(span);
            var content = Encoding.UTF8.GetString(byteData);
            JsonUtility.FromJsonOverwrite(content, value);
        }

        /// <inheritdoc/>
        public Task Serialize4UnityObjectAsync<T>(Stream buffer, T obj) where T : UnityEngine.Object
        {
            var content = JsonUtility.ToJson(obj);
            return buffer.WriteAsync(Encoding.UTF8.GetBytes(content)).AsTask();
        }
    }
}
