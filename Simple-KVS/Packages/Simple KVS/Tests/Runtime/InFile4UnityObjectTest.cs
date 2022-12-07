using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace kumaS.SimpleKVS.Tests
{
    public class InFile4UnityObjectTest
    {
        [UnityTest]
        public IEnumerator InFile4UnityObjectTestSet0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            Assert.That(() => InFileKVS4UnityObject<JsonUtilitySerializer>.Set("key00", data), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleMonoData_key00.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleMonoData_key00.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleMonoData_key00.kvs"));
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectTestSet1()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            Assert.That(() => InFileKVS4UnityObject<JsonUtilitySerializer>.Set("key00", data, Application.temporaryCachePath), Throws.Nothing);
            Assert.That(File.Exists(Path.Combine(Application.temporaryCachePath, "SampleMonoData_key00.kvs")), Is.True);
            File.Delete(Path.Combine(Application.temporaryCachePath, "SampleMonoData_key00.kvs"));
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectTestGet0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            InFileKVS4UnityObject<JsonUtilitySerializer>.Set("key01", data);

            var rcv = GameObject.Find("SampleData2").GetComponent<SampleMonoData>();
            InFileKVS4UnityObject<JsonUtilitySerializer>.Get("key01", rcv);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            InFileKVS4UnityObject<JsonUtilitySerializer>.Get("key01", rcv);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            InFileKVS4UnityObject<JsonUtilitySerializer>.Get("key01", rcv, true);
            rcv.id = "";
            rcv.number = 0;
            Assert.That(() => InFileKVS4UnityObject<JsonUtilitySerializer>.Get("key01", rcv), Throws.Exception);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectTestTryGet0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            InFileKVS4UnityObject<JsonUtilitySerializer>.Set("key02", data);

            var rcv = GameObject.Find("SampleData2").GetComponent<SampleMonoData>();
            var isStored = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGet("key02", rcv);
            Assert.That(isStored, Is.True);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            isStored = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGet("key02", rcv);
            Assert.That(isStored, Is.True);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            isStored = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGet("key02", rcv, true);
            isStored = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGet("key02", rcv);
            Assert.That(isStored, Is.False);
            rcv.id = "";
            rcv.number = 0;
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectUtilTestSetAsync0()
        {

            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            var task = InFileKVS4UnityObject<JsonUtilitySerializer>.SetAsync("key04", data);
            yield return task.AsEnumerator();

            Assert.That(File.Exists(Path.Combine(Application.dataPath, "SampleMonoData_key04.kvs")), Is.True);
            Assert.That(File.ReadAllText(Path.Combine(Application.dataPath, "SampleMonoData_key04.kvs")), Is.EqualTo("{\"id\":\"Simple-KVS\",\"number\":1}"));
            File.Delete(Path.Combine(Application.dataPath, "SampleMonoData_key04.kvs"));

            Assert.That(true);
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectUtilTestGetAsync0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            var task = InFileKVS4UnityObject<JsonUtilitySerializer>.SetAsync("key05", data);
            yield return task.AsEnumerator();

            var rcv = GameObject.Find("SampleData2").GetComponent<SampleMonoData>();
            var task2 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key05", rcv);
            yield return task2.AsEnumerator();
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            var task3 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key05", rcv);
            yield return task3.AsEnumerator();
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            var task4 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key05", rcv, true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key05", rcv, true);
            yield return task5.AsEnumerator(false);
            Assert.That(task5.IsFaulted, Is.True);
            rcv.id = "";
            rcv.number = 0;
        }

        [UnityTest]
        public IEnumerator InFile4UnityObjectTestTryGetAsync0()
        {
            SceneManager.LoadScene("SendTest");
            for (var i = 0; i < 10; i++)
            {
                yield return null;
            }
            var data = GameObject.Find("SampleData").GetComponent<SampleMonoData>();

            var task = InFileKVS4UnityObject<JsonUtilitySerializer>.SetAsync("key06", data);
            yield return task.AsEnumerator();

            var rcv = GameObject.Find("SampleData2").GetComponent<SampleMonoData>();
            var task2 = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGetAsync("key06", rcv);
            yield return task2.AsEnumerator();
            Assert.That(task2.Result, Is.True);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            var task3 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key06", rcv);
            yield return task3.AsEnumerator();
            Assert.That(task2.Result, Is.True);
            Assert.That(rcv.id, Is.EqualTo(data.id));
            Assert.That(rcv.number, Is.EqualTo(data.number));
            rcv.id = "";
            rcv.number = 0;
            var task4 = InFileKVS4UnityObject<JsonUtilitySerializer>.GetAsync("key06", rcv, true);
            yield return task4.AsEnumerator();
            var task5 = InFileKVS4UnityObject<JsonUtilitySerializer>.TryGetAsync("key06", rcv);
            yield return task5.AsEnumerator();
            Assert.That(task5.Result, Is.False);
            rcv.id = "";
            rcv.number = 0;
        }
    }
}
