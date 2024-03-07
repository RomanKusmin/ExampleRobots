// Author: Roman Kuzmin, email: jetacore@gmail.com
// Created: 2021/02/05

#if UNITY_EDITOR

using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace EvaArchitecture.Plugins.UltraFastReferenceFinder.EditorLogic
{
    public static class AssetUtils
    {
        public static UnityObject LoadAssetByGuid(string guid)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return LoadAssetByPath(path);
        }
        
        public static UnityObject LoadAssetByPath(string path)
        {
            return AssetDatabase.LoadAssetAtPath(path, typeof(UnityObject));
        }
    }
}

#endif
