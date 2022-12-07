using NUnit.Framework;

using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace kumaS.SimpleKVS.Tests
{
    public class InMemorySceneTest
    {
        [Test]
        public void InMemorySceneTest0()
        {
            SceneManager.LoadScene("SendTest");
            var data = new SampleData()
            {
                id = "Simple-KVS",
                number = 1,
            };
            InMemoryKVS<SampleData>.Set("key00", data);
            SceneManager.LoadScene("ReceiveTest");
            Assert.That(InMemoryKVS<SampleData>.Get("key00"), Is.EqualTo(data));
        }

        [UnityTest]
        public IEnumerator InMemorySceneTest1()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();
            Assert.That(() => InMemoryKVS<SampleMonoData>.Set("key01", data), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InMemory4UnitySceneTest0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();
            InMemoryKVS4UnityObject<SampleMonoData>.Set("key01", data);
            SceneManager.LoadScene("ReceiveTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var setData = GameObject.Find("SampleData").GetComponent<SampleMonoData>();
            InMemoryKVS4UnityObject<SampleMonoData>.Get("key01", setData);
            Assert.That(setData.id, Is.EqualTo(data.id));
            Assert.That(setData.number, Is.EqualTo(data.number));
        }
    }
}

