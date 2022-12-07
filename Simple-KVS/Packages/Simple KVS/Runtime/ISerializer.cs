using System.IO;
using System.Threading.Tasks;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface for serialization and deserialization used in <see cref="InFileKVS{S}"/>.</para>
    /// <para><see cref="InFileKVS{S}"/>で用いるシリアライズ・デシリアライズのためのインターフェース。</para>
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// <para>Serialize data.</para>
        /// <para>データをシリアル化する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="buffer">
        /// <para>Buffer to write serialized data.</para>
        /// <para>シリアル化したデータの書き込み先。</para>
        /// </param>
        /// <param name="obj">
        /// <para>Data for serialize.</para>
        /// <para>シリアル化するデータ。</para>
        /// </param>
        public void Serialize<T>(Stream buffer, T obj);

        /// <summary>
        /// <para>Serialize data.</para>
        /// <para>データをシリアル化する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="buffer">
        /// <para>Buffer to write serialized data.</para>
        /// <para>シリアル化したデータの書き込み先。</para>
        /// </param>
        /// <param name="obj">
        /// <para>Data for serialize.</para>
        /// <para>シリアル化するデータ。</para>
        /// </param>
        /// <returns>
        /// <para>Serialized data.</para>
        /// <para>シリアル化されたデータ。</para>
        /// </returns>
        public Task SerializeAsync<T>(Stream buffer, T obj);

        /// <summary>
        /// <para>Deserialize data.</para>
        /// <para>データをデシリアル化する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>シリアル化されたデータ。</para>
        /// </param>
        /// <returns>
        /// <para>Deserialized data.</para>
        /// <para>デシリアル化されたデータ。</para>
        /// </returns>
        public T Deserialize<T>(Stream data);

        /// <summary>
        /// <para>Deserialize data.</para>
        /// <para>データをデシリアル化する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>シリアル化されたデータ。</para>
        /// </param>
        /// <returns>
        /// <para>Deserialized data.</para>
        /// <para>デシリアル化されたデータ。</para>
        /// </returns>
        public ValueTask<T> DeserializeAsync<T>(Stream data);
    }
}
