using System.Collections.Generic;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on memory for <see cref="UnityEngine.Object"/>.</para>
    /// <para>メモリ上で動作する<see cref="UnityEngine.Object"/>用のKey Value Store。</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryKVS4UnityObject<T> where T : ISerializable
    {
        private static readonly Dictionary<string, object> m_store = new Dictionary<string, object>();

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>保存した値を取得する。デフォルトでは値を取得後、その値は除去される。</para>
        /// </summary>
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
        public static void Get(string key, T value, bool remove = true)
        {
            var val = m_store[key];
            if (remove)
            {
                m_store.Remove(key);
            }
            value.Deserialize(val);
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
        public static bool TryGet(string key, T value, bool remove = true)
        {
            var isStored = m_store.TryGetValue(key, out var val);
            if (isStored && remove)
            {
                m_store.Remove(key);
            }
            if (isStored)
            {
                value.Deserialize(val);
            }
            return isStored;
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
            m_store[key] = value.Serialize();
        }
    }
}
