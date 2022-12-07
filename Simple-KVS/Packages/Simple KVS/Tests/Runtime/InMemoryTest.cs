using NUnit.Framework;

using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine;

namespace kumaS.SimpleKVS.Tests
{
    public class InMemoryTest
    {
        [Test]
        public void InMemoryTestSet0()
        {
            var data = new SampleData() { 
                id = "Simple-KVS",
                number = 1,
            };

            Assert.That(() => InMemoryKVS<SampleData>.Set("key0", data), Throws.Nothing);
        }

        [Test]
        public void InMemoryTestGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key1", data);
            Assert.That(InMemoryKVS<SampleData>.Get("key1"), Is.EqualTo(data));
            Assert.That(() => InMemoryKVS<SampleData>.Get("key1"), Throws.Exception);
        }

        [Test]
        public void InMemoryTestGet1()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key2", data);
            Assert.That(InMemoryKVS<SampleData>.Get("key2", false), Is.EqualTo(data));
            Assert.That(() => InMemoryKVS<SampleData>.Get("key2"), Throws.Nothing);
        }

        [Test]
        public void InMemoryTestGet2()
        {
            Assert.That(() => InMemoryKVS<SampleData>.Get("nokey"), Throws.Exception);
        }

        [Test]
        public void InMemoryTestTryGet0()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key3", data);
            var isStored = InMemoryKVS<SampleData>.TryGet("key3", out var ret);
            Assert.That(ret, Is.EqualTo(data));
            Assert.That(isStored, Is.True);
            Assert.That(InMemoryKVS<SampleData>.TryGet("key3", out var _), Is.False);
        }

        [Test]
        public void InMemoryTestTryGet1()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key4", data);
            var isStored = InMemoryKVS<SampleData>.TryGet("key4", out var ret, false);
            Assert.That(ret, Is.EqualTo(data));
            Assert.That(isStored, Is.True);
            Assert.That(InMemoryKVS<SampleData>.TryGet("key4", out var _), Is.True);
        }

        [Test]
        public void InMemoryTestTryGet2()
        {
            Assert.That(InMemoryKVS<SampleData>.TryGet("nokey", out var _), Is.False);
        }

        [Test]
        public void InMemoryTestTryGet3()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key5", data);
            var (isStored, ret) = InMemoryKVS<SampleData>.TryGet("key5");
            Assert.That(ret, Is.EqualTo(data));
            Assert.That(isStored, Is.True);
            var (isStored1, _) = InMemoryKVS<SampleData>.TryGet("key5");
            Assert.That(isStored1, Is.False);
        }

        [Test]
        public void InMemoryTestTryGet4()
        {
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key6", data);
            var (isStored, ret) = InMemoryKVS<SampleData>.TryGet("key6", false);
            Assert.That(ret, Is.EqualTo(data));
            Assert.That(isStored, Is.True);
            var (isStored1, _) = InMemoryKVS<SampleData>.TryGet("key6");
            Assert.That(isStored1, Is.True);
        }

        [Test]
        public void InMemoryTestTryGet5()
        {
            var (isStored, _) = InMemoryKVS<SampleData>.TryGet("nokey");
            Assert.That(isStored, Is.False);
        }
    }
}
