using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.JsonDotNet;

namespace kumaS.SimpleKVS.Tests.External.JsonDotNet
{
    public class JsonDotNetTest
    {
        [Test]
        public void InFileDotTestSet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<JsonDotNetSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleData_key0.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleData_key0.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleData_key0.kvs"));
        }

        [Test]
        public void InFileDotTestGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<JsonDotNetSerializer>.Set("key1", data);
            var d = InFileKVS<JsonDotNetSerializer>.Get<SampleData>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<JsonDotNetSerializer>.Get<SampleData>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<JsonDotNetSerializer>.Get<SampleData>("key1", true);
            Assert.That(() => InFileKVS<JsonDotNetSerializer>.Get<SampleData>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileDotTestSetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<JsonDotNetSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleData_key2.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleData_key2.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleData_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileDotTestGetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<JsonDotNetSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<JsonDotNetSerializer>.GetAsync<SampleData>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<JsonDotNetSerializer>.GetAsync<SampleData>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<JsonDotNetSerializer>.GetAsync<SampleData>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<JsonDotNetSerializer>.GetAsync<SampleData>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }
    }
}
