using MessagePack;

using System;

namespace kumaS.SimpleKVS.Tests.External.MsgPack
{
    [MessagePackObject]
    [Serializable]
    public class SampleData4
    {
        [Key(0)]
        public string id;
        [Key(1)]
        public int number;
    }
}
