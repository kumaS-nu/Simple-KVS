using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.TestTools;

namespace kumaS.SimpleKVS.Tests
{
    public class InFileTest
    {
        [Test]
        public void InFileUnilTestSet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<JsonUtilitySerializer>.Set("key00", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key00.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.persistentDataPath, "SampleData_key00.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key00.kvs"));
        }

        [Test]
        public void InFileUnilTestSet1()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<JsonUtilitySerializer>.Set("key00", data, Application.temporaryCachePath), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.temporaryCachePath, "SampleData_key00.kvs")), Is.True);
            File.Delete(Path.Combine(Application.temporaryCachePath, "SampleData_key00.kvs"));
        }

        [Test]
        public void InFileXmlTestSet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<XMLSerializer>.Set("key20", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key20.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.persistentDataPath, "SampleData_key20.kvs")).Replace("\r",""), Is.EqualTo("<?xml version=\"1.0\"?>\n<SampleData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n  <id>Simple-KVS</id>\n  <number>1</number>\n</SampleData>"));
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key20.kvs"));
        }

        [Test]
        public void InFileUtilTestGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<JsonUtilitySerializer>.Set("key01", data);
            var d = InFileKVS<JsonUtilitySerializer>.Get<SampleData>("key01");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<JsonUtilitySerializer>.Get<SampleData>("key01");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<JsonUtilitySerializer>.Get<SampleData>("key01", true);
            Assert.That(() => InFileKVS<JsonUtilitySerializer>.Get<SampleData>("key01"), Throws.Exception);
        }

        [Test]
        public void InFileXmlTestGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<XMLSerializer>.Set("key21", data);
            var d = InFileKVS<XMLSerializer>.Get<SampleData>("key21");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<XMLSerializer>.Get<SampleData>("key21");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<XMLSerializer>.Get<SampleData>("key21", true);
            Assert.That(() => InFileKVS<XMLSerializer>.Get<SampleData>("key21"), Throws.Exception);
        }

        [Test]
        public void InFileTestTryGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<JsonUtilitySerializer>.Set("key02", data);
            var isStored = InFileKVS<JsonUtilitySerializer>.TryGet<SampleData>("key02", out var val);
            Assert.That(isStored, Is.True);
            Assert.That(val.id, Is.EqualTo(data.id));
            Assert.That(val.number, Is.EqualTo(data.number));
            isStored = InFileKVS<JsonUtilitySerializer>.TryGet("key02", out val);
            Assert.That(isStored, Is.True);
            Assert.That(val.id, Is.EqualTo(data.id));
            Assert.That(val.number, Is.EqualTo(data.number));
            isStored = InFileKVS<JsonUtilitySerializer>.TryGet("key02", out val, true);
            isStored = InFileKVS<JsonUtilitySerializer>.TryGet("key02", out val);
            Assert.That(isStored, Is.False);
        }

        [Test]
        public void InFileTestTryGet1()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<JsonUtilitySerializer>.Set("key03", data);
            var (isStored, val) = InFileKVS<JsonUtilitySerializer>.TryGet<SampleData>("key03");
            Assert.That(isStored, Is.True);
            Assert.That(val.id, Is.EqualTo(data.id));
            Assert.That(val.number, Is.EqualTo(data.number));
            (isStored, val) = InFileKVS<JsonUtilitySerializer>.TryGet<SampleData>("key03");
            Assert.That(isStored, Is.True);
            Assert.That(val.id, Is.EqualTo(data.id));
            Assert.That(val.number, Is.EqualTo(data.number));
            (isStored, val) = InFileKVS<JsonUtilitySerializer>.TryGet<SampleData>("key03", true);
            (isStored, val) = InFileKVS<JsonUtilitySerializer>.TryGet<SampleData>("key03");
            Assert.That(isStored, Is.False);
        }

        [UnityTest]
        public IEnumerator InFileUtilTestSetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<JsonUtilitySerializer>.SetAsync("key04", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key04.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.persistentDataPath, "SampleData_key04.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key04.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileXmlTestSetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<XMLSerializer>.SetAsync("key24", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData_key24.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.persistentDataPath, "SampleData_key24.kvs")).Replace("\r", ""), Is.EqualTo("<?xml version=\"1.0\"?>\n<SampleData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n  <id>Simple-KVS</id>\n  <number>1</number>\n</SampleData>"));
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData_key24.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileUtilTestGetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<JsonUtilitySerializer>.SetAsync("key05", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key05");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key05");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key05", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key05", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }

        [UnityTest]
        public IEnumerator InFileXmlTestGetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<XMLSerializer>.SetAsync("key25", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<XMLSerializer>.GetAsync<SampleData>("key25");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<XMLSerializer>.GetAsync<SampleData>("key25");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<XMLSerializer>.GetAsync<SampleData>("key25", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<XMLSerializer>.GetAsync<SampleData>("key25", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }

        [UnityTest]
        public IEnumerator InFileTestTryGetAsync0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<JsonUtilitySerializer>.SetAsync("key06", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<JsonUtilitySerializer>.TryGetAsync<SampleData>("key06");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.isStored, Is.True);
            Assert.That(task2.Result.value.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.value.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key06");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.isStored, Is.True);
            Assert.That(task2.Result.value.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.value.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<JsonUtilitySerializer>.GetAsync<SampleData>("key06", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<JsonUtilitySerializer>.TryGetAsync<SampleData>("key06");
            yield return task5.AsEnumerator();
            Assert.That(task5.Result.isStored, Is.False);
        }
    }

    public static class ValueTaskExtension
    {
        public static IEnumerator AsEnumerator(this ValueTask task, bool throwException = true)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (throwException && task.IsFaulted)
            {
                throw task.AsTask().Exception;
            }
        }

        public static IEnumerator AsEnumerator<T>(this ValueTask<T> task, bool throwException = true)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (throwException && task.IsFaulted)
            {
                throw task.AsTask().Exception;
            }
        }
    }

    public static class TaskExtension
    {
        public static IEnumerator AsEnumerator(this Task task, bool throwException = true)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (throwException && task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public static IEnumerator AsEnumerator<T>(this Task<T> task, bool throwException = true)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (throwException && task.IsFaulted)
            {
                throw task.Exception;
            }
        }
    }
}
