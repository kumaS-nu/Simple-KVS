using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    public class ReceiveData : MonoBehaviour
    {
        [SerializeField]
        private DataContent m_dc;

        [SerializeField]
        private ComplexData m_cd;

        // Start is called before the first frame update
        void Start()
        {
            InMemoryKVS4UnityObject<DataContent>.Get("key", m_dc);
            InMemoryKVS4UnityObject<ComplexData>.Get("key", m_cd);
        }
    }
}
