#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using ProtoBuf;

namespace kumaS.SimpleKVS.Tests.External.Protobuf
{
    [ProtoContract]
    public class SampleData3
    {
        [ProtoMember(1)]
        public string id;
        [ProtoMember(2)]
        public int number;
    }
}

#endif
