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
    /// <para>ファイル上で動作するKey Value Store。</para>
    /// </summary>
    /// <typeparam name="S">
    /// <para>Serializer / Deserializer.</para>
    /// <para>シリアライザー・デシリアライザー。</para>
    /// </typeparam>
    public static class InFileKVS<S> where S : ISerializer, new()
    {
        private static readonly S serializer = new S();

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>保存した値を取得する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
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
        /// <para>Get stored value. Saved at Application.persistentDataPath.</para>
        /// <para>保存した値を取得する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static T Get<T>(string key, bool remove = false)
        {
            return Get<T>(key, Application.persistentDataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>保存した値を取得する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
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
        /// <para>Get stored value. Saved at Application.persistentDataPath.</para>
        /// <para>保存した値を取得する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static ValueTask<T> GetAsync<T>(string key, bool remove = false)
        {
            return GetAsync<T>(key, Application.persistentDataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>保存した値を取得する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>値が保存されていたか。</para>
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
        /// <para>Get stored value. Saved at Application.persistentDataPath.</para>
        /// <para>保存した値を取得する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Stored value.</para>
        /// <para>保存していた値。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>値が保存されていたか。</para>
        /// </returns>
        public static bool TryGet<T>(string key, out T value, bool remove = false)
        {
            return TryGet(key, out value, Application.persistentDataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>保存した値を取得する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>値が保存されていたか。保存していた値。</para>
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
        /// <para>Get stored value. Saved at Application.persistentDataPath.</para>
        /// <para>保存した値を取得する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>値が保存されていたか。保存していた値。</para>
        /// </returns>
        public static (bool isStored, T value) TryGet<T>(string key, bool remove = false)
        {
            return TryGet<T>(key, Application.persistentDataPath, remove);
        }

        /// <summary>
        /// <para>Get stored value.</para>
        /// <para>保存した値を取得する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>値が保存されていたか。保存していた値。</para>
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
        /// <para>Get stored value. Saved at Application.persistentDataPath.</para>
        /// <para>保存した値を取得する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>値が保存されていたか。保存していた値。</para>
        /// </returns>
        public static ValueTask<(bool isStored, T value)> TryGetAsync<T>(string key, bool remove = false)
        {
            return TryGetAsync<T>(key, Application.persistentDataPath, remove);
        }

        /// <summary>
        /// <para>Store value.</para>
        /// <para>値を保存する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>保存する値。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
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
        /// <para>Store value. Saved at Application.persistentDataPath.</para>
        /// <para>値を保存する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>保存する値。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        public static void Set<T>(string key, T value)
        {
            Set(key, value, Application.persistentDataPath);
        }

        /// <summary>
        /// <para>Store value.</para>
        /// <para>値を保存する。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>保存する値。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
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
        /// <para>Store value. Saved at Application.persistentDataPath.</para>
        /// <para>値を保存する。保存先はApplication.persistentDataPath。</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Data type to get.</para>
        /// <para>取得するデータ型。</para>
        /// </typeparam>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>保存する値。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
        /// </param>
        public static ValueTask SetAsync<T>(string key, T value)
        {
            return SetAsync(key, value, Application.persistentDataPath);
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
