#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using System.IO;
using System.Threading.Tasks;

using ProtoBuf;

using UnityEngine;

namespace kumaS.SimpleKVS.External.ProtoBuf
{
    /// <summary>
    /// <para>Wrapper for ProtoBuf-net <see cref="Serializer"/>.</para>
    /// <para>ProtoBuf-net <see cref="Serializer"/>のラッパー。</para>
    /// </summary>
    public sealed class ProtoBufSerializer : ISerializer, ISerializer4UnityObject
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            return Serializer.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            Serializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public void Serialize4UnityObject<T>(Stream buffer, T obj) where T : Object
        {
            Serializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public void Deserialize4UnityObject<T>(Stream data, T value) where T : Object
        {
            Serializer.Deserialize(data, value);
        }

        /// <inheritdoc/>
#pragma warning disable CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます
        public async ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            return Serializer.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public async Task SerializeAsync<T>(Stream buffer, T obj)
        {
            Serializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public async Task Serialize4UnityObjectAsync<T>(Stream buffer, T obj) where T : Object
        {
            Serializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public async ValueTask Deserialize4UnityObjectAsync<T>(Stream data, T value) where T : Object
        {
            Serializer.Deserialize<T>(data, value);
        }
#pragma warning restore CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます
    }
}

#endif
