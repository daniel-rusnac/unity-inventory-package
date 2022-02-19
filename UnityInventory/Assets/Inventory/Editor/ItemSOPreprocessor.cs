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

            if (t == null || !t.IsDefined(typeof(ItemSO), true))
                return AssetDeleteResult.DidNotDelete;

            ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>(assetPath);
            ReferenceDatabaseCollectionAllSO.RemoveDatabaseItem(item);

            return AssetDeleteResult.DidNotDelete;
        }
    }
}