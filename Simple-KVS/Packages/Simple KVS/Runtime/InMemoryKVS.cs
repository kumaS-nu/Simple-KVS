using System.Collections.Generic;

using UnityEngine;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on memory. This can not use for subclass of <see cref="Object"/> . If you want to use for subclass of <see cref="Object"/> , use <see cref="InMemoryKVS4UnityObject{T}"/>.</para>
    /// <para>��������œ��삷��Key Value Store�B<see cref="Object"/>���p�����Ă���Ǝg���Ȃ��B���̏ꍇ��<see cref="InMemoryKVS4UnityObject{T}"/>���g�p�B</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Type you want to save.</para>
    /// <para>�ۑ�����^�B</para>
    /// </typeparam>
    public static class InMemoryKVS<T>
    {
        private static readonly Dictionary<string, T> m_store = new Dictionary<string, T>();

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>�ۑ������l���擾����B�f�t�H���g�ł͒l���擾��A���̒l�͏��������B</para>
        /// </summary>
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
        /// <para>�ۑ������l���擾����B�f�t�H���g�ł͒l���擾��A���̒l�͏��������B</para>
        /// </summary>
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
        /// <para>�ۑ������l���擾����B�f�t�H���g�ł͒l���擾��A���̒l�͏��������B</para>
        /// </summary>
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
        /// <para>�l��ۑ�����B</para>
        /// </summary>
        /// <param name="key">
        /// <para>key</para>
        /// <para>�L�[</para>
        /// </param>
        /// <param name="value">
        /// <para>Storeing value.</para>
        /// <para>�ۑ�����l�B</para>
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
