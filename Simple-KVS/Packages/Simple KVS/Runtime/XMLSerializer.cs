using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Wrapper for <see cref="XmlSerializer"/>.</para>
    /// <para><see cref="XmlSerializer"/>XmlSerializerのラッパー。</para>
    /// </summary>
    public class XMLSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(data);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(buffer, obj);
        }

#pragma warning disable CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます
        /// <inheritdoc/>
        public async ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(data);
        }

        /// <inheritdoc/>
        public async Task SerializeAsync<T>(Stream buffer, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(buffer, obj);
        }
#pragma warning restore CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます
    }
}
