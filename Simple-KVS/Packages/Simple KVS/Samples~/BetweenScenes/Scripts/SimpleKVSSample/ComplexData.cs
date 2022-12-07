using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    /// <summary>
    /// <para>MonoBehaviour with complex data to be sent.</para>
    /// <para>���肽�����G�ȃf�[�^������MonoBehaviour�B</para>
    /// </summary>
    public sealed class ComplexData : MonoBehaviour, ISerializable
    {
        [SerializeField]
        private string item0;

        [SerializeField]
        private int item1;

        [SerializeField]
        private float item2;

        [SerializeField]
        private double item3;

        [SerializeField]
        private List<string> item4;

        public void Deserialize(object data)
        {
            // Desirialize writes like this.
            // �f�V���A���C�Y�͂����B
            var memStream = (MemoryStream)data;
            var serializer = new JsonUtilitySerializer();
            serializer.Deserialize4UnityObject(memStream, this);
        }

        public object Serialize()
        {
            // If you write it like this, you can send data if it can be serialized with JsonUtility.
            // ����������JsonUtility�ŃV���A���C�Y�ł���΃f�[�^���邱�Ƃ��ł���B
            var memStream = new MemoryStream();
            var serializer = new JsonUtilitySerializer();
            serializer.Serialize4UnityObject(memStream, this);
            memStream.Position = 0;
            return memStream;
        }
    }
}
