namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Interface to send / receive <see cref="UnityEngine.Object"/> in <see cref="InMemoryKVS4UnityObject{T}"/>.</para>
    /// <para><see cref="InMemoryKVS4UnityObject{T}"/>でUnityの<see cref="UnityEngine.Object"/>をやりとりするためのインターフェース。</para>
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// <para>Serialize this instance.</para>
        /// <para>このインスタンスをシリアライズする。</para>
        /// </summary>
        /// <returns></returns>
        public object Serialize();

        /// <summary>
        /// <para>Deserialize data and set to this instance.</para>
        /// <para>データをデシリアライズしこのインスタンスに反映させる。</para>
        /// </summary>
        /// <param name="data">
        /// <para>Serialized data.</para>
        /// <para>シリアライズされたデータ。</para>
        /// </param>
        public void Deserialize(object data);
    }
}
