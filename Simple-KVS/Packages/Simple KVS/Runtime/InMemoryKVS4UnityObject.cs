using System.Collections.Generic;

namespace kumaS.SimpleKVS
{
    /// <summary>
    /// <para>Key Value Store works on memory for <see cref="UnityEngine.Object"/>.</para>
    /// <para>��������œ��삷��<see cref="UnityEngine.Object"/>�p��Key Value Store�B</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryKVS4UnityObject<T> where T : ISerializable
    {
        private static readonly Dictionary<string, object> m_store = new Dictionary<string, object>();

        /// <summary>
        /// <para>Get stored value. By default, after get value, it will be removed.</para>
        /// <para>�ۑ������l���擾����B�f�t�H���g�ł͒l���擾��A���̒l�͏��������B</para>
        /// </summary>
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
        /// <para>�ۑ������l���擾����B�f�t�H���g�ł͒l���擾��A���̒l�͏��������B</para>
        /// </summary>
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
            m_store[key] = value.Serialize();
        }
    }
}
