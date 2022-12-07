using System;

namespace kumaS.SimpleKVS.Tests
{
    [Serializable]
    public class SampleData
    {
        public string id;
        public int number;
    }

    [Serializable]
    public class SampleData2
    {
        public string id { get; set; }
        public int number { get; set; }
    }
}
