using System;
using InventorySystem.InventoryDatabase;
using UnityEditor;

namespace InventorySystem
{
    public class ItemSOPreprocessor : UnityEditor.AssetModificationProcessor
    {
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            Type t = AssetDatabase.GetMainAssetTypeAtPath(assetPath);

            if (t == null)
                return AssetDeleteResult.DidNotDelete;

            if (t.IsAssignableFrom(typeof(ItemSO)) || t.IsSubclassOf(typeof(ItemSO)))
            {
                ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>(assetPath);
                AssetPostprocessorDatabase.RemoveDatabaseItem(item);
            }
            
            return AssetDeleteResult.DidNotDelete;
        }
    }
}