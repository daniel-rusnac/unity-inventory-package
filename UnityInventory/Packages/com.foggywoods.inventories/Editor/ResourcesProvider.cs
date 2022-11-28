using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace FoggyWoods.Inventories
{
    public class ResourcesProvider
    {
        internal static VisualTreeAsset ItemDefinitionUxml { get; }
        internal static VisualTreeAsset ItemPropertyUxml { get; }
        
        static ResourcesProvider()
        {
            ItemDefinitionUxml = LoadAssetRelativeToScript<ResourcesProvider, VisualTreeAsset>("UI/ItemDefinitionUxml.uxml");
            ItemPropertyUxml = LoadAssetRelativeToScript<ResourcesProvider, VisualTreeAsset>("UI/ItemPropertyUxml.uxml");
        }

        private static string GetScriptParentDirectory<T>()
        {
            string[] guids = AssetDatabase.FindAssets($"t: Script ResourcesProvider");

            Assert.IsNotNull(guids);
            Assert.IsTrue(guids.Length > 0);

            string relativePath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return Path.GetDirectoryName(relativePath);
        }

        private static TAsset LoadAssetRelativeToScript<TScript, TAsset>(string relativePath) where TAsset : Object
        {
            string path = GetScriptParentDirectory<TScript>();
            path = Path.Combine(path, relativePath);
            return AssetDatabase.LoadAssetAtPath<TAsset>(path);
        }
    }
}