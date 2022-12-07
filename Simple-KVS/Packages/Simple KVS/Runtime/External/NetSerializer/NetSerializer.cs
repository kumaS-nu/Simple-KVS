#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using NetSerializer;

using System;
using System.IO;
using System.Threading.Tasks;

namespace kumaS.SimpleKVS.External.Net
{
    /// <summary>
    /// <para>Wrapper for NetSerializer.</para>
    /// <para>NetSerializer�̃��b�p�[�B</para>
    /// </summary>
    public sealed class NetSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            var serializer = new Serializer(new Type[] { typeof(T) });
            return (T)serializer.Deserialize(data);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            var serializer = new Serializer(new Type[] { typeof(T) });
            serializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
#pragma warning disable CS1998 // �񓯊����\�b�h�́A'await' ���Z�q���Ȃ����߁A�����I�Ɏ��s����܂�
        public async ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            var serializer = new Serializer(new Type[] { typeof(T) });
            return (T)serializer.Deserialize(data);
        }

        /// <inheritdoc/>
        public async Task SerializeAsync<T>(Stream buffer, T obj)
        {
            var serializer = new Serializer(new Type[] { typeof(T) });
            serializer.Serialize(buffer, obj);
        }
#pragma warning restore CS1998 // �񓯊����\�b�h�́A'await' ���Z�q���Ȃ����߁A�����I�Ɏ��s����܂�
    }
}

#endif
