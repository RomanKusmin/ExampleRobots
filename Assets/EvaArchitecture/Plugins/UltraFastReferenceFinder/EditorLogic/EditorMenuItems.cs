// Author: Roman Kuzmin, email: jetacore@gmail.com
// Created: 2021/02/05

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using EvaArchitecture.Plugins.UltraFastReferenceFinder.Logics;
using UnityEditor;
using UnityEngine;

namespace EvaArchitecture.Plugins.UltraFastReferenceFinder.EditorLogic
{
    public static class EditorMenuItems
    {
        [MenuItem("Assets/Find References In Project (GLOBAL Fast) Ctrl + back quote %`", priority = 25)]
        private static void FindRefsGlobalMenuItem()
        {
            FindRefsInProject(true, true);
        }
        
        [MenuItem("Assets/Find References In Project (GLOBAL Reload cache)", priority = 26)]
        private static void FindRefsGlobalReloadCacheMenuItem()
        {
            FindRefsInProject(false, true);
        }

        [MenuItem("Assets/Find References In Project (Current root dir Fast) %q", priority = 27)]
        private static void FindRefsMenuItem()
        {
            FindRefsInProject(true, false);
        }
        
        [MenuItem("Assets/Find References In Project (Current root dir Reload cache)", priority = 28)]
        private static void FindRefsReloadCacheMenuItem()
        {
            FindRefsInProject(false, false);
        }

        public static List<GameObject> FindRefsInProject(
            bool useCache, 
            bool global, 
            bool showResultWindow = true, 
            bool mustReturnResult = false)
        {
            try
            {
                if (Selection.activeObject == null)
                    return null;
                
                var sourceGuid = Selection.assetGUIDs[0];
                var sourceFilePath = AssetDatabase.GUIDToAssetPath(sourceGuid);

                var inFolder = Application.dataPath;
                
                if (!global)
                {
                    var startIndex = "Assets/".Length;
                    var endIndex = sourceFilePath.IndexOf("/", startIndex, StringComparison.Ordinal);
                    if (endIndex <= 0)
                        return null;
                    
                    var subFolder = sourceFilePath.Substring(startIndex, endIndex - startIndex);
                    inFolder += "/" + subFolder;
                }

                List<GameObject> foundAssets = null;

                RefsFinder.FindRefsForFile(sourceGuid, sourceFilePath, inFolder, useCache, InternalOnProgress,
                    result =>
                    {
                        var pars = new RefsFinderPars();

                        pars.RefsFinderResult.Refs.Clear();
                        
                        if (result.FoundObjects != null && result.FoundObjects.Count > 0)
                        {
                            /*if (result.FoundObjects.Count == 1)
                            {
                                var refName = result.FoundObjects[0];
                                var refObject = AssetUtils.LoadAssetByPath(refName);
                                Selection.objects = new[] {refObject};
                            }*/

                            if (mustReturnResult)
                            {
                                foundAssets = new List<GameObject>();
                                if (result.FoundObjects != null
                                    && result.FoundObjects.Count > 0)
                                {
                                    foreach (var path in result.FoundObjects)
                                    {
                                        if (string.IsNullOrEmpty(path))
                                            continue;

                                        var asset = AssetUtils.LoadAssetByPath(path);
                                        if (asset == null)
                                            continue;

                                        var go = asset as GameObject;
                                        if (go == null)
                                            continue;

                                        if (foundAssets.Contains(go))
                                            continue;

                                        foundAssets.Add(go);
                                    }
                                }
                            }

                            pars.RefsFinderResult.Refs.Add(result.SourceGuid, result.FoundObjects);
                        }
                        
                        if (showResultWindow)
                            ResultWindow.Create(pars);
                    });

                return foundAssets;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static void InternalOnProgress(string info, RefsFinderProgress result)
        {
            var progress = result.MasterProgress <= 0f ? result.Progress : result.MasterProgress;
            result.IsCanceled = EditorUtility.DisplayCancelableProgressBar(info, "That could take a while... " + (progress * 100f) + " %", progress);
            if (result.IsCanceled)
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }
}

#endif
