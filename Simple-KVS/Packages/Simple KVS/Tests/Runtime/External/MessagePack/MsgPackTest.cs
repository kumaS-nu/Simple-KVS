using NUnit.Framework;

using System.Collections;
using System.IO;

using UnityEngine;
using UnityEngine.TestTools;

using kumaS.SimpleKVS.External.MsgPack;
using MessagePack.Resolvers;
using MessagePack;

namespace kumaS.SimpleKVS.Tests.External.MsgPack
{
    public class MsgPackTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            StaticCompositeResolver.Instance.Register(
                GeneratedResolver.Instance,
                MessagePack.Unity.UnityResolver.Instance,
                MessagePack.Unity.Extension.UnityBlitWithPrimitiveArrayResolver.Instance,
                StandardResolver.Instance
            );
            var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = option;
        }

        [Test]
        public void InFileMsgPackTestSet0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<MsgPackSerializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs"));
        }

        [Test]
        public void InFileMsgPackTestGet0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<MsgPackSerializer>.Set("key1", data);
            var d = InFileKVS<MsgPackSerializer>.Get<SampleData4>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<MsgPackSerializer>.Get<SampleData4>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<MsgPackSerializer>.Get<SampleData4>("key1", true);
            Assert.That(() => InFileKVS<MsgPackSerializer>.Get<SampleData4>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileMsgPackTestSetAsync0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MsgPackSerializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileMsgPackTestGetAsync0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MsgPackSerializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<MsgPackSerializer>.GetAsync<SampleData4>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<MsgPackSerializer>.GetAsync<SampleData4>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<MsgPackSerializer>.GetAsync<SampleData4>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<MsgPackSerializer>.GetAsync<SampleData4>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }

        [Test]
        public void InFileMsgPackLZ4TestSet0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InFileKVS<MsgPackLZ4Serializer>.Set("key0", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData4_key0.kvs"));
        }

        [Test]
        public void InFileMsgPackLZ4TestGet0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InFileKVS<MsgPackLZ4Serializer>.Set("key1", data);
            var d = InFileKVS<MsgPackLZ4Serializer>.Get<SampleData4>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            d = InFileKVS<MsgPackLZ4Serializer>.Get<SampleData4>("key1");
            Assert.That(d.id, Is.EqualTo(data.id));
            Assert.That(d.number, Is.EqualTo(data.number));
            InFileKVS<MsgPackLZ4Serializer>.Get<SampleData4>("key1", true);
            Assert.That(() => InFileKVS<MsgPackLZ4Serializer>.Get<SampleData4>("key1"), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFileMsgPackLZ4TestSetAsync0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MsgPackLZ4Serializer>.SetAsync("key2", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs")), Is.True);
            Assert.That(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs")).Length, Is.Not.Zero);
            File.Delete(Path.Combine(Application.persistentDataPath, "SampleData4_key2.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFileMsgPackLZ4TestGetAsync0()
        {
            var data = new SampleData4()
            {
                id = "Simple-KVS",
                number = 1,
            };
            var task = InFileKVS<MsgPackLZ4Serializer>.SetAsync("key3", data);
            yield return task.AsEnumerator();

            var task2 = InFileKVS<MsgPackLZ4Serializer>.GetAsync<SampleData4>("key3");
            yield return task2.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task3 = InFileKVS<MsgPackLZ4Serializer>.GetAsync<SampleData4>("key3");
            yield return task3.AsEnumerator();
            Assert.That(task2.Result.id, Is.EqualTo(data.id));
            Assert.That(task2.Result.number, Is.EqualTo(data.number));
            var task4 = InFileKVS<MsgPackLZ4Serializer>.GetAsync<SampleData4>("key3", true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS<MsgPackLZ4Serializer>.GetAsync<SampleData4>("key3", true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
        }
    }
}
