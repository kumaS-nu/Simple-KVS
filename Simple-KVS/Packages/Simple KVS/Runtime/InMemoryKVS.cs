using System.Collections.Generic;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on memory. This can not use for subclass of <see cref="Object"/> . If you want to use for subclass of <see cref="Object"/> , use <see cref="InMemoryKVS4UnityObject{T}"/>.</para>
    /// <para>メモリ上で動作するKey Value Store。<see cref="Object"/>を継承していると使えない。その場合は<see cref="InMemoryKVS4UnityObject{T}"/>を使用。</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Type you want to save.</para>
    /// <para>保存する型。</para>
    /// </typeparam>
    public static class InMemoryKVS<T>
    {
        private static readonly Dictionary<string, T> m_store = new Dictionary<string, T>();

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>保存した値を取得する。デフォルトでは値を取得後、その値は除去される。</para>
        /// </summary>
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
        public static T Get(string key, bool remove = true)
        {
            ThrowIfObject();

            var val = m_store[key];
            if (remove)
            {
                m_store.Remove(key);
            }
            return val;
        }

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>保存した値を取得する。デフォルトでは値を取得後、その値は除去される。</para>
        /// </summary>
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
        public static bool TryGet(string key, out T value, bool remove = true)
        {
            ThrowIfObject();

            var isStored = m_store.TryGetValue(key, out value);
            if (isStored && remove)
            {
                m_store.Remove(key);
            }
            return isStored;
        }

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>保存した値を取得する。デフォルトでは値を取得後、その値は除去される。</para>
        /// </summary>
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
        public static (bool isStored, T value) TryGet(string key, bool remove = true)
        {
            ThrowIfObject();

            var isStored = m_store.TryGetValue(key, out T value);
            if (isStored && remove)
            {
                m_store.Remove(key);
            }
            return (isStored, value);
        }

        /// <summary>
        /// <para>Store value.</para>
        /// <para>値を保存する。</para>
        /// </summary>
        /// <param name="key">
        /// <para>key</para>
        /// <para>キー</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>保存する値。</para>
        /// </param>
        public static void Set(string key, T value)
        {
            ThrowIfObject();
            m_store[key] = value;
        }

        private static void ThrowIfObject()
        {
            if (typeof(T).IsSubclassOf(typeof(Object)))
            {
                throw new System.TypeAccessException("This class can not use subclass of Object. i.e. MonoBehaviour. Use InMemoryKVS4UnityObject");
            }
        }
    }
}
