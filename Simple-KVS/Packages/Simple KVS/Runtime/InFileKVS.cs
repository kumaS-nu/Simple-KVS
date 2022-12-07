using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on file.</para>
    /// <para>�t�@�C����œ��삷��Key Value Store�B</para>
    /// </summary>
    /// <typeparam name="S">
    /// <para>Serializer / Deserializer.</para>
    /// <para>�V���A���C�U�[�E�f�V���A���C�U�[�B</para>
    /// </typeparam>
    public static class InFileKVS<S> where S : ISerializer, new()
    {
        private static readonly S serializer = new S();

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>�ۑ������l���擾����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static T Get<T>(string key, string rootPath, bool remove = false)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                throw new KeyNotFoundException($"The key : {key} is not found in type : {typeof(T).Name}.");
            }

            T value = default;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                value = serializer.Deserialize<T>(fs);
            }
            if (remove)
            {
                File.Delete(path);
            }
            return value;
        }

        /// <summary>
        /// <para>Get stored value. Saved at Application.dataPath.</para>
        /// <para>�ۑ������l���擾����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static T Get<T>(string key, bool remove = false)
        {
            return Get<T>(key, Application.dataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>�ۑ������l���擾����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static async ValueTask<T> GetAsync<T>(string key, string rootPath, bool remove = false)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                throw new KeyNotFoundException($"The key : {key} is not found in type : {typeof(T).Name}.");
            }

            T value = default;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                value = await serializer.DeserializeAsync<T>(fs);
            }
            if (remove)
            {
                File.Delete(path);
            }
            return value;
        }

        /// <summary>
        /// <para>Get stored value. Saved at Application.dataPath.</para>
        /// <para>�ۑ������l���擾����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static ValueTask<T> GetAsync<T>(string key, bool remove = false)
        {
            return GetAsync<T>(key, Application.dataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>�ۑ������l���擾����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>�l���ۑ�����Ă������B</para>
        /// </returns>
        public static bool TryGet<T>(string key, out T value, string rootPath, bool remove = false)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                value = default;
                return false;
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                value = serializer.Deserialize<T>(fs);
            }
            if (remove)
            {
                File.Delete(path);
            }
            return true;
        }

        /// <summary>
        /// <para>Get stored value. Saved at Application.dataPath.</para>
        /// <para>�ۑ������l���擾����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Stored value.</para>
        /// <para>�ۑ����Ă����l�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>�l���ۑ�����Ă������B</para>
        /// </returns>
        public static bool TryGet<T>(string key, out T value, bool remove = false)
        {
            return TryGet(key, out value, Application.dataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>�ۑ������l���擾����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>�l���ۑ�����Ă������B�ۑ����Ă����l�B</para>
        /// </returns>
        public static (bool isStored, T value) TryGet<T>(string key, string rootPath, bool remove = false)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                return (false, default);
            }

            T value = default;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                value = serializer.Deserialize<T>(fs);
            }
            if (remove)
            {
                File.Delete(path);
            }
            return (true, value);
        }

        /// <summary>
        /// <para>Get stored value. Saved at Application.dataPath.</para>
        /// <para>�ۑ������l���擾����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>�l���ۑ�����Ă������B�ۑ����Ă����l�B</para>
        /// </returns>
        public static (bool isStored, T value) TryGet<T>(string key, bool remove = false)
        {
            return TryGet<T>(key, Application.dataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>�ۑ������l���擾����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>�l���ۑ�����Ă������B�ۑ����Ă����l�B</para>
        /// </returns>
        public static async ValueTask<(bool isStored, T value)> TryGetAsync<T>(string key, string rootPath, bool remove = false)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                return (false, default);
            }

            T value = default;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                value = await serializer.DeserializeAsync<T>(fs);
            }
            if (remove)
            {
                File.Delete(path);
            }
            return (true, value);
        }

        /// <summary>
        /// <para>Get stored value. Saved at Application.dataPath.</para>
        /// <para>�ۑ������l���擾����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>�l���ۑ�����Ă������B�ۑ����Ă����l�B</para>
        /// </returns>
        public static ValueTask<(bool isStored, T value)> TryGetAsync<T>(string key, bool remove = false)
        {
            return TryGetAsync<T>(key, Application.dataPath, remove);
        }

        /// <summary>
        /// <para>Store value.</para>
        /// <para>�l��ۑ�����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>�ۑ�����l�B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        public static void Set<T>(string key, T value, string rootPath)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, value);
                fs.Flush();
            }
        }

        /// <summary>
        /// <para>Store value. Saved at Application.dataPath.</para>
        /// <para>�l��ۑ�����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>�ۑ�����l�B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        public static void Set<T>(string key, T value)
        {
            Set(key, value, Application.dataPath);
        }

        /// <summary>
        /// <para>Store value.</para>
        /// <para>�l��ۑ�����B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>�ۑ�����l�B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        public static async ValueTask SetAsync<T>(string key, T value, string rootPath)
        {
            ThrowIfObject(typeof(T));

            var path = GetPath(typeof(T), rootPath, key);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                await serializer.SerializeAsync(fs, value);
                await fs.FlushAsync();
            }
        }

        /// <summary>
        /// <para>Store value. Saved at Application.dataPath.</para>
        /// <para>�l��ۑ�����B�ۑ����Application.dataPath�B</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>�擾����f�[�^�^�B</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>�ۑ�����l�B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        public static ValueTask SetAsync<T>(string key, T value)
        {
            return SetAsync(key, value, Application.dataPath);
        }

        private static string GetPath(Type t, string rootPath, string key)
        {
            var sb = new StringBuilder();
            sb.Append(t.Name);
            sb.Append("_");
            sb.Append(key);
            sb.Append(".kvs");
            return Path.Combine(rootPath, sb.ToString());
        }

        private static void ThrowIfObject(Type t)
        {
            if (t.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                throw new System.TypeAccessException("This class can not use subclass of Object. i.e. MonoBehaviour. Use InFIleKVS4UnityObject");
            }
        }
    }
}
