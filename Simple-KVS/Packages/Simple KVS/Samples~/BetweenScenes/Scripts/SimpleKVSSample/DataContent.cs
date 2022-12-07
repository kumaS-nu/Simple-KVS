using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    /// <summary>
    /// <para>MonoBehaviour with data to be sent.</para>
    /// <para>送りたいデータを持つMonoBehaviour。</para>
    /// </summary>
    public sealed class DataContent : MonoBehaviour, ISerializable
    {
        public string data;

        public void Deserialize(object data)
        {
            // Appliy to the object from the data you send in this function.
            // この関数内で送ったデータからオブジェクトに反映。
            var d = (Data)data;
            this.data = d.data;
        }

        public object Serialize()
        {
            // Convert to the data you want to send (not Unity.Object) from this object in this function.
            // この関数内でこのオブジェクトから送りたいデータ（Unity.Objectでない）に変換。
            var d = new Data() { data = data };
            return d;
        }
    }

    /// <summary>
    /// <para>Data content.</para>
    /// <para>データの中身。</para>
    /// </summary>
    public sealed class Data {
        public string data;
    }
}
