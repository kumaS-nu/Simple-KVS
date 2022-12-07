using UnityEngine;

namespace kumaS.SimpleKVS.Tests
{
    public class SampleMonoData : MonoBehaviour, ISerializable
    {
        public string id;
        public int number;

        public void Deserialize(object data)
        {
            var d = (SMonoData)data;
            id = d.id;
            number = d.number;
        }

        public object Serialize()
        {
            return new SMonoData() { id = id, number = number };
        }
    }

    public class SMonoData
    {
        public string id;
        public int number;
    }
}
