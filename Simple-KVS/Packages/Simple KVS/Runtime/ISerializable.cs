namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface to send / receive <see cref="UnityEngine.Object"/> in <see cref="InMemoryKVS4UnityObject{T}"/>.</para>
    /// <para><see cref="InMemoryKVS4UnityObject{T}"/>��Unity��<see cref="UnityEngine.Object"/>�����Ƃ肷�邽�߂̃C���^�[�t�F�[�X�B</para>
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// <para>Serialize this instance.</para>
        /// <para>���̃C���X�^���X���V���A���C�Y����B</para>
        /// </summary>
        /// <returns></returns>
        public object Serialize();

        /// <summary>
        /// <para>Deserialize data and set to this instance.</para>
        /// <para>�f�[�^���f�V���A���C�Y�����̃C���X�^���X�ɔ��f������B</para>
        /// </summary>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>�V���A���C�Y���ꂽ�f�[�^�B</para>
        /// </param>
        public void Deserialize(object data);
    }
}
