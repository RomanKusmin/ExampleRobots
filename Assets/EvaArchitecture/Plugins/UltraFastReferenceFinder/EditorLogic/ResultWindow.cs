// Author: Roman Kuzmin, email: jetacore@gmail.com
// Created: 2021/02/05

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using EvaArchitecture._Bases;
using EvaArchitecture._Services.UiServices.UiService._Bases;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Plugins.UltraFastReferenceFinder.Helpers;
using EvaArchitecture.Plugins.UltraFastReferenceFinder.Logics;
using EvaArchitecture.Plugins.UltraFastReferenceFinder.Windows;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace EvaArchitecture.Plugins.UltraFastReferenceFinder.EditorLogic
{
    internal class ResultWindow : EditorWindow
    {
        private Vector2 _scrollPositionRefs;
        private static RefsFinderPars _pars;
        private static Dictionary<(string, UnityObject), List<(string, UnityObject)>> _refs;
        private static List<string> _rootPaths = new List<string>();
        private static GameObject _lastSelectedGameObject;
        private List<ObjectUsageResult> _usagesInComponents;

        public static void Create(RefsFinderPars pars)
        {
            _refs = null;
            
            _pars = pars;
            if (_pars == null)
                return;

            _refs = new Dictionary<(string, UnityObject), List<(string, UnityObject)>>();

            var refs = pars.RefsFinderResult.Refs;
            if (refs != null && refs.Count > 0)
            {
                foreach (var keyValue in refs)
                {
                    var key = keyValue.Key;
                    var value = keyValue.Value;
                    
                    if (string.IsNullOrEmpty(key))
                        continue;

                    var targetObject = AssetUtils.LoadAssetByGuid(key);
                    if (targetObject == null)
                        continue;

                    var refsFromObjects = value?.Select(item => (item, AssetUtils.LoadAssetByPath(item))).ToList();

                    var path = AssetDatabase.GUIDToAssetPath(key);
                    _refs.Add((path, targetObject), refsFromObjects);
                }
            }

            _rootPaths = new List<string>();

            var window = GetWindow<ResultWindow>("References");
        }

        private void OnGUI()
        {
            if (_pars == null)
                return;
            
            EditorGUILayout.Space();

            ShowDependencies();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("--------------------------------------------");
        }

        private void ShowDependencies()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("--------------------------------------------");

            EditorGUILayout.EndVertical();

            if (_refs == null || _refs.Count == 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("There are NO dependencies");

                return;
            }

            EditorGUILayout.Space();

            _scrollPositionRefs = EditorGUILayout.BeginScrollView(_scrollPositionRefs, false, true,
                new GUILayoutOption[] {GUILayout.MinHeight(580)});

            if (_rootPaths == null || _rootPaths.Count == 0)
            {
                _rootPaths = new List<string>();
                foreach (var item in _refs)
                {
                    var refs = item.Value;

                    foreach (var (path, obj) in refs)
                    {
                        var rootPath = GetRootPath(path);
                            
                        if (!_rootPaths.Contains(rootPath))
                            _rootPaths.Add(rootPath);
                    }
                }
                _rootPaths.Sort();
            }

            var isFirstPath = true;
            foreach (var rootPath in _rootPaths)
            {
                var isFirstItem = true;
                foreach (var item in _refs)
                {
                    var source = item.Key;
                    var refs = item.Value;

                    var (_, key) = source;
                    if (key == null)
                        continue;

                    if (isFirstPath)
                    {
                        isFirstPath = false;
                        
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("assets references -->>", GUILayout.MaxWidth(150f));
                        EditorGUILayout.Space();

                        EditorGUILayout.ObjectField(key, typeof(UnityObject), false);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.EndVertical();
                        
                        var selected = Selection.activeObject as GameObject;
                        if (selected.IsNull())
                        {
                            EditorGUILayout.LabelField($"Please select a dependent object to see usage in components", GUILayout.MaxWidth(1000f));
                        }
                        else
                        {
                            if (key is EvaEvent evaEvent)
                                ShowEventUsagesInComponents(evaEvent);
                            else
                                ShowEventUsagesInComponents(key);

                        }
                    }

                    EditorGUILayout.Space(5f);

                    if (refs == null)
                        continue;

                    if (isFirstItem)
                    {
                        isFirstItem = false;
                     
                        EditorGUILayout.LabelField($"{rootPath}", GUILayout.MaxWidth(250f));
                        EditorGUILayout.Space();
                    }

                    foreach (var (refPath, obj) in refs)
                    {
                        var refRootPath = GetRootPath(refPath);
                        if (refRootPath != rootPath)
                            continue;
                        
                        EditorGUILayout.ObjectField(obj, typeof(UnityObject), false);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void ShowEventUsagesInComponents<T>(T evaEvent)
            where T : UnityEngine.Object
        {
            if (evaEvent.IsNull())
                return;
            
            var selected = Selection.activeObject as GameObject;
            if (selected.IsNull())
                return;
            
            if (_lastSelectedGameObject != selected)
            {
                _lastSelectedGameObject = selected;
                _usagesInComponents = FindEventInComponents(evaEvent);
            }

            if (_usagesInComponents.IsNull() || _usagesInComponents.Count == 0)
            {
                EditorGUILayout.Space(5f);
                EditorGUILayout.LabelField($"Usages NOT found", GUILayout.MaxWidth(150f));
            }
            else
            {
                EditorGUILayout.Space(5f);
                EditorGUILayout.LabelField($"Used in Components:", GUILayout.MaxWidth(150f));

                foreach (var usage in _usagesInComponents)
                {
                    if (usage.IsNull())
                        continue;

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField($"Usage Count = {usage.Count}", GUILayout.MaxWidth(150f));
                    EditorGUILayout.ObjectField(usage.GameObject, typeof(UnityEngine.Object), false,
                        GUILayout.MinWidth(200f));
                    EditorGUILayout.LabelField($"{usage.ComponentsNames}", GUILayout.MaxWidth(1500f));

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        
        public static List<ObjectUsageResult> FindEventInComponents<T>(T obj)
            where T : UnityEngine.Object
        {
            if (obj.IsNull())
                return null;
            
            var selection = Selection.activeObject as GameObject;
            if (selection.IsNull())
                return null;

            var components = selection.GetComponentsInChildren<BaseUiEvaControl>(true);
            if (components.IsNull() || components.Length == 0) 
                return null;
            
            var foundGameObjects = new List<ObjectUsageResult>();
            foreach (var component in components)
            {
                if (component.IsNull())
                    continue;

                if (obj is EvaEvent evaEvent)
                {
                    if (!evaEvent.ContainsEvaEvent(component))
                        continue;
                }
                else if (obj is System.Object o)
                {
                    if (!o.EvaEventContainsModel(component))
                        continue;
                }

                var go = component.gameObject;
                var existing = foundGameObjects.FirstOrDefault(
                    item => !item.IsNull() && item.GameObject == go);

                var componentName = component.GetType().Name;
                    
                if (existing.IsNull())
                {
                    var item = new ObjectUsageResult(go, 1, componentName);
                    foundGameObjects.Add(item);
                }
                else
                {
                    existing.Count++;
                    existing.ComponentsNames += ", " + componentName;
                }
            }

            return foundGameObjects;
        }

        private string GetRootPath(string path)
        {
            var rootPath = path.Replace("\\", "/");
            if (rootPath.StartsWith("Assets/"))
                rootPath = rootPath.Substring("Assets/".Length);

            var pos = rootPath.IndexOf("/", StringComparison.Ordinal);
            if (pos > 0)
                rootPath = rootPath.Substring(0, pos);

            return "Assets/" + rootPath;
        }
    }
}

#endif
