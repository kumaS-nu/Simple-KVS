using System.IO;
using System.Threading.Tasks;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface for serialization and deserialization used in <see cref="InFileKVS{S}"/>.</para>
    /// <para><see cref="InFileKVS{S}"/>�ŗp����V���A���C�Y�E�f�V���A���C�Y�̂��߂̃C���^�[�t�F�[�X�B</para>
    /// </summary>
    public interface ISerializer
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
        public void Serialize<T>(Stream buffer, T obj);

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
        public Task SerializeAsync<T>(Stream buffer, T obj);

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
        /// <returns>
        /// <para>Deserialized data.</para>
        /// <para>�f�V���A�������ꂽ�f�[�^�B</para>
        /// </returns>
        public T Deserialize<T>(Stream data);

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
        /// <returns>
        /// <para>Deserialized data.</para>
        /// <para>�f�V���A�������ꂽ�f�[�^�B</para>
        /// </returns>
        public ValueTask<T> DeserializeAsync<T>(Stream data);
    }
}
