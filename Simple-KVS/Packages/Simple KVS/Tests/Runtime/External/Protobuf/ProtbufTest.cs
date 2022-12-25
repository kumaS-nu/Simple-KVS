#if !USE_NUGET_IMPORTER || NUGET_PACKAGE_READY

using NUnit.Framework;

using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.ProtoBuf;
using UnityEngine.SceneManagement;

namespace kumaS.SimpleKVS.Tests.External.Protobuf {
    public class ProtoBufSerializerTest
    {
        [Test]
        public void InFileProtoBufSerializerTestSet0()
        {
            var data = new SampleData3()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<ProtoBufSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData3_key0.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData3_key0.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData3_key0.kvs"));
        }

        [Test]
        public void InFileProtoBufSerializerTestGet0()
        {
            var data = new SampleData3()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<ProtoBufSerializer>.Set("key1", data);
            var d = InFileKVS<ProtoBufSerializer>.Get<SampleData3>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<ProtoBufSerializer>.Get<SampleData3>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<ProtoBufSerializer>.Get<SampleData3>("key1", true);
            Assert.That(() => InFileKVS<ProtoBufSerializer>.Get<SampleData3>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileProtoBufSerializerTestSetAsync0()
        {
            var data = new SampleData3()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<ProtoBufSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData3_key2.kvs")), Is.True);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData3_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileProtoBufSerializerTestGetAsync0()
        {
            var data = new SampleData3()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<ProtoBufSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<ProtoBufSerializer>.GetAsync<SampleData3>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<ProtoBufSerializer>.GetAsync<SampleData3>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<ProtoBufSerializer>.GetAsync<SampleData3>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<ProtoBufSerializer>.GetAsync<SampleData3>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectProtoBufSerializerTestSet0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").AddComponent<SampleMonoData3>();
            data.id = "Simple-KVS";
            data.number = 1;

            Assert.That(() => InFileKVS4UnityObject<ProtoBufSerializer>.Set("key00", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key00.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key00.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key00.kvs"));
            Object.Destroy(data);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectProtoBufSerializerTestGet0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").AddComponent<SampleMonoData3>();
            data.id = "Simple-KVS";
            data.number = 1;

            InFileKVS4UnityObject<ProtoBufSerializer>.Set("key01", data);

            var rcv = GameObject.Find("SampleData2").AddComponent<SampleMonoData3>();
            InFileKVS4UnityObject<ProtoBufSerializer>.Get("key01", rcv);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            InFileKVS4UnityObject<ProtoBufSerializer>.Get("key01", rcv, true);
            Object.Destroy(data);
            Object.Destroy(rcv);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectProtoBufSerializerTestSetAsync0()
        {

            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").AddComponent<SampleMonoData3>();
            data.id = "Simple-KVS";
            data.number = 1;

            var task = InFileKVS4UnityObject<ProtoBufSerializer>.SetAsync("key04", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key04.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key04.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleMonoData3_key04.kvs"));
            Object.Destroy(data);

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectProtoBufSerializerTestGetAsync0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").AddComponent<SampleMonoData3>();
            data.id = "Simple-KVS";
            data.number = 1;

            var task = InFileKVS4UnityObject<ProtoBufSerializer>.SetAsync("key05", data);
            yield return task.AsEnumerator();

            var rcv = GameObject.Find("SampleData2").AddComponent<SampleMonoData3>();
            var task2 = InFileKVS4UnityObject<ProtoBufSerializer>.GetAsync("key05", rcv);
            yield return task2.AsEnumerator();
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));

            InFileKVS4UnityObject<ProtoBufSerializer>.Get("key05", rcv, true);

            Object.Destroy(data);
            Object.Destroy(rcv);
        }
    }
}

#endif