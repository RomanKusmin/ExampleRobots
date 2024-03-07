// Author: Roman Kuzmin, email: jetacore@gmail.com
// Created: 2021/02/05

#if UNITY_EDITOR

using System;
using EvaArchitecture.Plugins.UltraFastReferenceFinder.Logics;
using UnityEditor;

namespace EvaArchitecture.Plugins.UltraFastReferenceFinder.EditorLogic
{
    public static class RefsFinder
    {
        public static bool FindRefsForFile(
            string sourceGuid, 
            string sourceFilePath,
            string inPath,
            bool useCache,
            Action<string, RefsFinderProgress> onProgress,
            Action<RefsFinderProgress> onResult)
        {
            if (string.IsNullOrEmpty(sourceGuid))
                throw new Exception($"Guid is empty for file={sourceFilePath}");

            if (!InternalDo(sourceGuid,
                    sourceFilePath,
                    inPath,
                    useCache,
                    onProgress,
                    onResult))
                return false;

            return true;
        }

        private static bool FindByGuidDependency(
            string sourceGuid, 
            Action<string, RefsFinderProgress> onProgress, 
            RefsFinderProgress result)
        {
            var sourcePath = AssetDatabase.GUIDToAssetPath(sourceGuid);
            var assetGuids = AssetDatabase.FindAssets(string.Empty);
            var count = assetGuids.Length;
            var actionsPerFrame = count.Cbrt().ToInt().CutBefore(1);

            for (var i = 0; i < count; i++)
            {
                if (result.IsCanceled)
                    break;

                result.Progress = (i + 1f) / count;

                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
                var dependencies = AssetDatabase.GetDependencies(assetPath);
                
                foreach (var dependency in dependencies)
                {
                    var dependencyGuid = AssetDatabase.AssetPathToGUID(dependency);

                    if (dependencyGuid == sourceGuid && dependencyGuid != assetGuids[i])
                    {
                        result.UnsortedFoundObjects.Add(assetPath);
                    }
                }

                if (i % actionsPerFrame == 0)
                {
                    onProgress?.Invoke(RefsFinderLogic.GetInfoFileProgress(sourcePath), result);
                }
                
                if (result.IsCanceled)
                    return false;
            }

            result.IsFinished = true;
            return true;
        }
        
        private static float Cbrt(this int value)
        {
            return (float)Math.Pow(value, 1d / 3d);
        }
        
        private static int ToInt(this float value)
        {
            return (int)value;
        }
        
        private static int CutBefore(this int value, int min)
        {
            return value < min ? min : value;
        }
        
        private static bool InternalDo(
            string sourceGuid,
            string sourceFilePath,
            string inPath,
            bool useCache,
            Action<string, RefsFinderProgress> onProgress,
            Action<RefsFinderProgress> onResult)
        {
            var result = new RefsFinderProgress {SourceGuid = sourceGuid};

            if (EditorSettings.serializationMode != SerializationMode.ForceText)
            {
                if (!FindByGuidDependency(sourceGuid, onProgress, result))
                    return false;
            }
            else
            {
                if (!RefsFinderLogic.InternalFind(sourceGuid, sourceFilePath, inPath, useCache, onProgress, result))
                    return false;
            }

            onResult?.Invoke(result);
            
            return true;
        }
    }
}

#endif
