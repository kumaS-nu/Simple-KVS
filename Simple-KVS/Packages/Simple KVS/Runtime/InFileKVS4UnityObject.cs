using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on file for <see cref="UnityEngine.Object"/>.</para>
    /// <para>�t�@�C����œ��삷��<see cref="UnityEngine.Object"/>�p��Key Value Store�B</para>
    /// </summary>
    /// <typeparam name="S">
    /// <para>Serializer / Deserializer.</para>
    /// <para>�V���A���C�U�[�E�f�V���A���C�U�[�B</para>
    /// </typeparam>
    public static class InFileKVS4UnityObject<S> where S : ISerializer4UnityObject, new()
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
        /// <param name="value">
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>�f�[�^��ۑ�����p�X�B</para>
        /// </param>
        /// <exception cref="KeyNotFoundException"></exception>
        public static void Get<T>(string key, T value, string rootPath, bool remove = false) where T : UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                throw new KeyNotFoundException($"The key : {key} is not found in type : {typeof(T).Name}.");
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                serializer.Deserialize4UnityObject(fs, value);
            }
            if (remove)
            {
                File.Delete(path);
            }
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <exception cref="KeyNotFoundException"></exception>
        public static void Get<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            Get(key, value, Application.dataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
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
        public static async ValueTask GetAsync<T>(string key, T value, string rootPath, bool remove = false) where T: UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                throw new KeyNotFoundException($"The key : {key} is not found in type : {typeof(T).Name}.");
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                await serializer.Deserialize4UnityObjectAsync(fs, value);
            }
            if (remove)
            {
                File.Delete(path);
            }
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
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
        public static ValueTask GetAsync<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return GetAsync(key, value, Application.dataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
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
        public static bool TryGet<T>(string key, T value, string rootPath, bool remove = false) where T: UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                return false;
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                serializer.Deserialize4UnityObject(fs, value);
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>�l���ۑ�����Ă������B</para>
        /// </returns>
        public static bool TryGet<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return TryGet(key, value, Application.dataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
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
        public static async ValueTask<bool> TryGetAsync<T>(string key, T value, string rootPath, bool remove = false) where T: UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            if (!File.Exists(path))
            {
                return false;
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                await serializer.Deserialize4UnityObjectAsync(fs, value);
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
        /// <para>Object to set value.</para>
        /// <para>�l��ݒ肷��I�u�W�F�N�g�B</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>�l���擾�㏜�����邩�ǂ����B</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>�l���ۑ�����Ă������B�ۑ����Ă����l�B</para>
        /// </returns>
        public static ValueTask<bool> TryGetAsync<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return TryGetAsync(key, value, Application.dataPath, remove);
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
        public static void Set<T>(string key, T value, string rootPath) where T : UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize4UnityObject(fs, value);
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
        public static void Set<T>(string key, T value) where T: UnityEngine.Object
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
        public static async ValueTask SetAsync<T>(string key, T value, string rootPath) where T: UnityEngine.Object
        {
            var path = GetPath(typeof(T), rootPath, key);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                await serializer.Serialize4UnityObjectAsync(fs, value);
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
        public static ValueTask SetAsync<T>(string key, T value) where T: UnityEngine.Object
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
    }
}
