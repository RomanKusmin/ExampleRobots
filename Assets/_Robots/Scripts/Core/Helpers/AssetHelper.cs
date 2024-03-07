using EvaArchitecture.EvaHelpers;
using UnityEngine;

namespace Core.Helpers
{
    public static class AssetHelper
    {
        public static GameObject CreateUiFromPrefab(
            this Transform parent,
            GameObject prefab)
        {
            if (prefab.IsNull())
                return null;

            var go = UnityEngine.Object.Instantiate(prefab, parent);
            go.name = go.name.TrimEnd("(Clone)");
            return go;
        }
        
        public static GameObject CreateFromPrefab(
            this Transform parent,
            GameObject prefab,
            bool mustResetPositionRotation = true,
            bool mustResetScale = false)
        {
            if (prefab.IsNull())
                return null;

            var go = UnityEngine.Object.Instantiate(prefab, parent);
            var tr = go.transform;
            if (mustResetPositionRotation)
            {
                tr.localPosition = Vector3.zero;
                tr.localRotation = Quaternion.identity;
            }

            go.name = go.name.TrimEnd("(Clone)");

            if (mustResetScale)
                tr.localScale = Vector3.one;

            return go;
        }
    }
}
