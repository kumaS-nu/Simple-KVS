#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using NUnit.Framework;

using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.TextDotJson;

namespace kumaS.SimpleKVS.Tests.External.TextDotJson
{
    public class TextDotJsonTest
    {
        [Test]
        public void InFileTextDotJsonTestSet0()
        {
            var data = new SampleData2()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<TextDotJsonSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleData2_key0.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleData2_key0.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleData2_key0.kvs"));
        }

        [Test]
        public void InFileTextDotJsonTestGet0()
        {
            var data = new SampleData2()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<TextDotJsonSerializer>.Set("key1", data);
            var d = InFileKVS<TextDotJsonSerializer>.Get<SampleData2>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<TextDotJsonSerializer>.Get<SampleData2>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<TextDotJsonSerializer>.Get<SampleData2>("key1", true);
            Assert.That(() => InFileKVS<TextDotJsonSerializer>.Get<SampleData2>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileTextDotJsonTestSetAsync0()
        {
            var data = new SampleData2()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<TextDotJsonSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleData2_key2.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleData2_key2.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleData2_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileTextDotJsonTestGetAsync0()
        {
            var data = new SampleData2()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<TextDotJsonSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<TextDotJsonSerializer>.GetAsync<SampleData2>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<TextDotJsonSerializer>.GetAsync<SampleData2>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<TextDotJsonSerializer>.GetAsync<SampleData2>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<TextDotJsonSerializer>.GetAsync<SampleData2>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }
    }
}

#endif