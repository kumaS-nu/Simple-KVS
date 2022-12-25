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
    /// <para>ファイル上で動作する<see cref="UnityEngine.Object"/>用のKey Value Store。</para>
    /// </summary>
    /// <typeparam name="S">
    /// <para>Serializer / Deserializer.</para>
    /// <para>シリアライザー・デシリアライザー。</para>
    /// </typeparam>
    public static class InFileKVS4UnityObject<S> where S : ISerializer4UnityObject, new()
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
        /// <param name="value">
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <param name="rootPath">
        /// <para>Path to save data.</para>
        /// <para>データを保存するパス。</para>
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <exception cref="KeyNotFoundException"></exception>
        public static void Get<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            Get(key, value, Application.persistentDataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
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
        public static ValueTask GetAsync<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return GetAsync(key, value, Application.persistentDataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?</para>
        /// <para>値が保存されていたか。</para>
        /// </returns>
        public static bool TryGet<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return TryGet(key, value, Application.persistentDataPath, remove);
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
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
        /// <para>Object to set value.</para>
        /// <para>値を設定するオブジェクト。</para>
        /// </param>
        /// <param name="remove">
        /// <para>After get value, it will be removed or not.</para>
        /// <para>値を取得後除去するかどうか。</para>
        /// </param>
        /// <returns>
        /// <para>Does value stored?. Stored value.</para>
        /// <para>値が保存されていたか。保存していた値。</para>
        /// </returns>
        public static ValueTask<bool> TryGetAsync<T>(string key, T value, bool remove = false) where T: UnityEngine.Object
        {
            return TryGetAsync(key, value, Application.persistentDataPath, remove);
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
        public static void Set<T>(string key, T value) where T: UnityEngine.Object
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
        public static ValueTask SetAsync<T>(string key, T value) where T: UnityEngine.Object
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
    }
}
