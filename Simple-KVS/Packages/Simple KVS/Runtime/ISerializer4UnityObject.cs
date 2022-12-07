using System.IO;
using System.Threading.Tasks;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface for serialization and deserialization<see cref="Object"/> used in InFileKVS.</para>
    /// <para>InFileKVS�ŗp����Unity��<see cref="Object"/>�p�̃V���A���C�Y�E�f�V���A���C�Y�̂��߂̃C���^�[�t�F�[�X�B</para>
    /// </summary>
    public interface ISerializer4UnityObject
    {
        /// <summary>
        /// <para>Serialize data.</para>
        /// <para>�f�[�^���V���A��������B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="buffer">
        /// <para>Buffer to write serialized data.</para>
        /// <para>�V���A���������f�[�^�̏������ݐ�B</para>
        /// </param>
        /// <param name="obj">
        /// <para>Data for serialize.</para>
        /// <para>�V���A��������f�[�^�B</para>
        /// </param>
        public void Serialize4UnityObject<T>(Stream buffer, T obj) where T: Object;

        /// <summary>
        /// <para>Serialize data.</para>
        /// <para>�f�[�^���V���A��������B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="buffer">
        /// <para>Buffer to write serialized data.</para>
        /// <para>�V���A���������f�[�^�̏������ݐ�B</para>
        /// </param>
        /// <param name="obj">
        /// <para>Data for serialize.</para>
        /// <para>�V���A��������f�[�^�B</para>
        /// </param>
        /// <returns>
        /// <para>Serialized data.</para>
        /// <para>�V���A�������ꂽ�f�[�^�B</para>
        /// </returns>
        public Task Serialize4UnityObjectAsync<T>(Stream buffer, T obj) where T : Object;

        /// <summary>
        /// <para>Deserialize data.</para>
        /// <para>�f�[�^���f�V���A��������B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>�V���A�������ꂽ�f�[�^�B</para>
        /// </param>
        /// <param name="value">
        /// <para>Object to which the value is applied.</para>
        /// <para>�K�p����I�u�W�F�N�g�B</para>
        /// </param>
        public void Deserialize4UnityObject<T>(Stream data, T value) where T : Object;

        /// <summary>
        /// <para>Deserialize data.</para>
        /// <para>�f�[�^���f�V���A��������B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type.</para>
        /// </typeparam>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>�V���A�������ꂽ�f�[�^�B</para>
        /// </param>
        /// <param name="value">
        /// <para>Object to which the value is applied.</para>
        /// <para>�K�p����I�u�W�F�N�g�B</para>
        /// </param>
        public ValueTask Deserialize4UnityObjectAsync<T>(Stream data, T value) where T : Object;
    }
}
