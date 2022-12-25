#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY 

using NUnit.Framework;

using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.Net;

namespace kumaS.SimpleKVS.Tests.External.Net
{
    public class NetSerializerTest
    {
        [Test]
        public void InFileNetSerializerTestSet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<NetSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key0.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData_key0.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key0.kvs"));
        }

        [Test]
        public void InFileNetSerializerTestGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<NetSerializer>.Set("key1", data);
            var d = InFileKVS<NetSerializer>.Get<SampleData>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<NetSerializer>.Get<SampleData>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<NetSerializer>.Get<SampleData>("key1", true);
            Assert.That(() => InFileKVS<NetSerializer>.Get<SampleData>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileNetSerializerTestSetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<NetSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key2.kvs")), Is.True);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileNetSerializerTestGetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<NetSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<NetSerializer>.GetAsync<SampleData>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<NetSerializer>.GetAsync<SampleData>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<NetSerializer>.GetAsync<SampleData>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<NetSerializer>.GetAsync<SampleData>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }
    }
}

#endif