using NUnit.Framework;

using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.MemPack;

namespace kumaS.SimpleKVS.Tests.External.MemPack
{
    public class MemPackTest
    {
        [Test]
        public void InFileMemPackTestSet0()
        {
            var data = new SampleData5()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<MemPackSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData5_key0.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData5_key0.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData5_key0.kvs"));
        }

        [Test]
        public void InFileMemPackTestGet0()
        {
            var data = new SampleData5()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<MemPackSerializer>.Set("key1", data);
            var d = InFileKVS<MemPackSerializer>.Get<SampleData5>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<MemPackSerializer>.Get<SampleData5>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<MemPackSerializer>.Get<SampleData5>("key1", true);
            Assert.That(() => InFileKVS<MemPackSerializer>.Get<SampleData5>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileMemPackTestSetAsync0()
        {
            var data = new SampleData5()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MemPackSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData5_key2.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData5_key2.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData5_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileMemPackTestGetAsync0()
        {
            var data = new SampleData5()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MemPackSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<MemPackSerializer>.GetAsync<SampleData5>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<MemPackSerializer>.GetAsync<SampleData5>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<MemPackSerializer>.GetAsync<SampleData5>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<MemPackSerializer>.GetAsync<SampleData5>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }
    }
}
