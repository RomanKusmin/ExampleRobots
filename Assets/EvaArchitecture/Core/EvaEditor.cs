#if UNITY_EDITOR

using System.Collections.Generic;
using EvaArchitecture._Bases;
using EvaArchitecture.Logger;
using UnityEditor;
using UnityEngine;

namespace EvaArchitecture.Core
{
    [CustomEditor(typeof(Eva))]
    public class EvaEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Set Event Configs"))
            {
                SetEventConfigs();
            }
        }

        private void SetEventConfigs()
        {
            var eventManager = Eva.Instance;
            if (eventManager == null)
            {
                Log.Error(() => $"Eva, SetEventConfigs, _instance is null. Please Eva into the scene");
                return;
            }
            
            var eventConfigs = new List<EvaEvent>();
            var assets = AssetDatabase.FindAssets("t:ScriptableObject");
            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (scriptableObject == null
                    || !(scriptableObject is EvaEvent baseEvent))
                    continue;

                eventConfigs.Add(baseEvent);
            }

            eventManager.SetEventConfigs(eventConfigs);
            EditorUtility.SetDirty(eventManager.EventManagerConfig);
        }
    }
}

#endif
