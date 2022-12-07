using UnityEngine;

namespace kumaS.SimpleKVS.Editor.External
{
    /// <summary>
    /// <para>Setting items to enable the wrapper for the external serializer.</para>
    /// <para>外部のシリアライザのラッパーの有効化の設定項目。</para>
    /// </summary>
    public sealed class ExternalControll
    {

        [SerializeField]
        internal bool m_jsonDotNet = false;
        internal const string KVS_ENABLE_JSONDOTNET = nameof(KVS_ENABLE_JSONDOTNET);

        [SerializeField]
        internal bool m_jsonText = false;
        internal const string KVS_ENABLE_JSONTEXT = nameof(KVS_ENABLE_JSONTEXT);

        [SerializeField]
        internal bool m_netserializer = false;
        internal const string KVS_ENABLE_NETSERIALIZER = nameof(KVS_ENABLE_NETSERIALIZER);

        [SerializeField]
        internal bool m_protobuf = false;
        internal const string KVS_ENABLE_PROTOBUF = nameof(KVS_ENABLE_PROTOBUF);

        [SerializeField]
        internal bool m_messagePack = false;
        internal const string KVS_ENABLE_MESSAGEPACK = nameof(KVS_ENABLE_MESSAGEPACK);

        [SerializeField]
        internal bool m_memoryPack = false;
        internal const string KVS_ENABLE_MEMORYPACK = nameof(KVS_ENABLE_MEMORYPACK);
    }
}
