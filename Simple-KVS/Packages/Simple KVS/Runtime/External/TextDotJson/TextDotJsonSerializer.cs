#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace kumaS.SimpleKVS.External.TextDotJson
{
    /// <summary>
    /// <para>Wrapper for System.Text.Json.<see cref="JsonSerializer"/>.</para>
    /// <para>System.Text.Json.<see cref="JsonSerializer"/>のラッパー。</para>
    /// </summary>
    public sealed class TextDotJsonSerializer : ISerializer
    {
        /// <inheritdoc/>
        public T Deserialize<T>(Stream data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public void Serialize<T>(Stream buffer, T obj)
        {
            JsonSerializer.Serialize(buffer, obj);
        }

        /// <inheritdoc/>
        public ValueTask<T> DeserializeAsync<T>(Stream data)
        {
            return JsonSerializer.DeserializeAsync<T>(data);
        }

        /// <inheritdoc/>
        public Task SerializeAsync<T>(Stream buffer, T obj)
        {
            return JsonSerializer.SerializeAsync(buffer, obj);
        }
    }
}

#endif
