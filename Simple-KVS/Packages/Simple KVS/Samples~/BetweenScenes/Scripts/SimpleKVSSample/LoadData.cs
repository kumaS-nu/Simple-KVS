using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    public class LoadData : MonoBehaviour
    {
        [SerializeField]
        private ComplexData m_cd;

        // Start is called before the first frame update
        void Start()
        {
            InFileKVS4UnityObject<JsonUtilitySerializer>.Get("key", m_cd);
        }
    }
}
