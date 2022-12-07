using UnityEngine;

namespace kumaS.SimpleKVS.Sample
{
    public class SaveData : MonoBehaviour
    {
        [SerializeField]
        private ComplexData m_cd;

        private void Awake()
        {
            InFileKVS4UnityObject<JsonUtilitySerializer>.Set("key", m_cd);
        }
    }
}
