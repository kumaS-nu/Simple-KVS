using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    /// <summary>
    /// <para>MonoBehaviour with data to be sent.</para>
    /// <para>���肽���f�[�^������MonoBehaviour�B</para>
    /// </summary>
    public sealed class DataContent : MonoBehaviour, ISerializable
    {
        public string data;

        public void Deserialize(object data)
        {
            // Appliy to the object from the data you send in this function.
            // ���̊֐����ő������f�[�^����I�u�W�F�N�g�ɔ��f�B
            var d = (Data)data;
            this.data = d.data;
        }

        public object Serialize()
        {
            // Convert to the data you want to send (not Unity.Object) from this object in this function.
            // ���̊֐����ł��̃I�u�W�F�N�g���瑗�肽���f�[�^�iUnity.Object�łȂ��j�ɕϊ��B
            var d = new Data() { data = data };
            return d;
        }
    }

    /// <summary>
    /// <para>Data content.</para>
    /// <para>�f�[�^�̒��g�B</para>
    /// </summary>
    public sealed class Data {
        public string data;
    }
}
