using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace FoggyWoods.Inventories
{
    public static class AssetDatabaseUtility
    {
        public static string GetScriptParentDirectory<T>()
        {
            string[] guids = AssetDatabase.FindAssets($"t: Script {nameof(T)}");

            Assert.IsNotNull(guids);
            Assert.IsTrue(guids.Length > 0);

            string relativePath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return Path.GetDirectoryName(relativePath);
        }

        public static TAsset LoadAssetRelativeToScript<TScript, TAsset>(string relativePath) where TAsset : Object
        {
            string path = GetScriptParentDirectory<TScript>();
            path = Path.Combine(path, relativePath);
            return AssetDatabase.LoadAssetAtPath<TAsset>(path);
        }
    }
}