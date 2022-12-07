using UnityEngine;
using UnityEngine.SceneManagement;

namespace kumaS.SimpleKVS.Sample
{
    public class SendData : MonoBehaviour
    {
        [SerializeField]
        private DataContent m_dc;

        [SerializeField]
        private ComplexData m_cd;

        [SerializeField]
        private int m_loadSceneNo;

        void Start()
        {
            InMemoryKVS4UnityObject<DataContent>.Set("key", m_dc);
            InMemoryKVS4UnityObject<ComplexData>.Set("key", m_cd);
            SceneManager.LoadScene(m_loadSceneNo);
        }
    }
}
