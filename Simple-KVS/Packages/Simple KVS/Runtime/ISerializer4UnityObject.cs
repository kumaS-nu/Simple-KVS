using System.IO;
using System.Threading.Tasks;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface for serialization and deserialization<see cref="Object"/> used in InFileKVS.</para>
    /// <para>InFileKVSで用いるUnityの<see cref="Object"/>用のシリアライズ・デシリアライズのためのインターフェース。</para>
    /// </summary>
    public interface ISerializer4UnityObject
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
        public void Serialize4UnityObject<T>(Stream buffer, T obj) where T: Object;

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
        public Task Serialize4UnityObjectAsync<T>(Stream buffer, T obj) where T : Object;

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
        /// <param name="value">
        /// <para>Object to which the value is applied.</para>
        /// <para>適用するオブジェクト。</para>
        /// </param>
        public void Deserialize4UnityObject<T>(Stream data, T value) where T : Object;

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
        /// <param name="value">
        /// <para>Object to which the value is applied.</para>
        /// <para>適用するオブジェクト。</para>
        /// </param>
        public ValueTask Deserialize4UnityObjectAsync<T>(Stream data, T value) where T : Object;
    }
}
