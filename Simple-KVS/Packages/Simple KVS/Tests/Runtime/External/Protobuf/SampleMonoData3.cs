#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using ProtoBuf;

using UnityEngine;

namespace kumaS.SimpleKVS.Tests.External.Protobuf
{
    [ProtoContract]
    public class SampleMonoData3 : MonoBehaviour
    {
        [ProtoMember(1)]
        public string id;
        [ProtoMember(2)]
        public int number;
    }
}

#endif
