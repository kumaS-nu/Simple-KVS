using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace kumaS.SimpleKVS.Editor.External
{
    /// <summary>
    /// <para>Window to configure the activation of the wrapper for the external serializer.</para>
    /// <paraを>外部のシリアライザのラッパーの有効化の設定するウィンドウ。</para>
    /// </summary>
    public sealed class ExternalControllWindow : EditorWindow
    {
        private static ExternalControll settings;
        private static ExternalControll Settings
        {
            get
            {
                if (settings == null)
                {
                    Load();
                }
                return settings;
            }
        }

        [MenuItem("Window/Simple KVS settings")]
        private static void ShowWindow()
        {
            Load();
            GetWindow<ExternalControllWindow>("Simple KVS settings");
        }

        [InitializeOnLoadMethod]
        public static void Load()
        {
            var (isSaved, data) = InFileKVS<JsonUtilitySerializer>.TryGet<ExternalControll>("savedata", Application.dataPath.Replace("Assets", "ProjectSettings"));
            if (isSaved)
            {
                settings = data;
            }
            else
            {
                settings = new ExternalControll();
            }
            SetSymbols();
        }

        private void OnGUI()
        {
            using (new EditorGUILayout.VerticalScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Enable external serializer", EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
                }

                Settings.m_jsonDotNet = GUILayout.Toggle(Settings.m_jsonDotNet, "Json.net (Newtonsoft.Json)");
                Settings.m_jsonText = GUILayout.Toggle(Settings.m_jsonText, "System.Text.Json");
                Settings.m_netserializer = GUILayout.Toggle(Settings.m_netserializer, "NetSerializer");
                Settings.m_protobuf = GUILayout.Toggle(Settings.m_protobuf, "Protbuf-net");
                Settings.m_messagePack = GUILayout.Toggle(Settings.m_messagePack, "MessagePack");
                Settings.m_memoryPack = GUILayout.Toggle(Settings.m_memoryPack, "MemoryPack");

                GUILayout.FlexibleSpace();

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Save"))
                    {
                        Save();
                        SetSymbols();
                    }
                }
            }
        }

        private static void SetSymbols()
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';').ToList();

            bool isChenged = false;

            isChenged = isChenged || SetSymbol(symbols, Settings.m_jsonDotNet, ExternalControll.KVS_ENABLE_JSONDOTNET);
            isChenged = isChenged || SetSymbol(symbols, Settings.m_jsonText, ExternalControll.KVS_ENABLE_JSONTEXT);
            isChenged = isChenged || SetSymbol(symbols, Settings.m_netserializer, ExternalControll.KVS_ENABLE_NETSERIALIZER);
            isChenged = isChenged || SetSymbol(symbols, Settings.m_protobuf, ExternalControll.KVS_ENABLE_PROTOBUF);
            isChenged = isChenged || SetSymbol(symbols, Settings.m_messagePack, ExternalControll.KVS_ENABLE_MESSAGEPACK);
            isChenged = isChenged || SetSymbol(symbols, Settings.m_memoryPack, ExternalControll.KVS_ENABLE_MEMORYPACK);

            if (isChenged)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", symbols));
            }
        }

        private static bool SetSymbol(List<string> symbols, bool isSet, string symbol)
        {
            if (symbols.Contains(symbol) == isSet)
            {
                return false;
            }
            else
            {
                if (isSet)
                {
                    symbols.Add(symbol);
                }
                else
                {
                    symbols.Remove(symbol);
                }
                return true;
            }
        }

        private static void Save()
        {
            InFileKVS<JsonUtilitySerializer>.Set("savedata", Settings, Application.dataPath.Replace("Assets", "ProjectSettings"));
        }
    }
}
