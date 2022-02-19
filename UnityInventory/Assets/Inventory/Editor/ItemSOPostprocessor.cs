using System;
using InventorySystem.InventoryDatabase;
using UnityEditor;

namespace InventorySystem
{
    public class ItemSOPostprocessor : AssetPostprocessor
    {
        public static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFromPaths)
        {
            foreach (string str in imported)
            {
                Type t = AssetDatabase.GetMainAssetTypeAtPath(str);

                if (!t.IsDefined(typeof(ItemSO), true)) 
                    continue;
                
                ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>(str);
                ReferenceDatabaseCollectionAllSO.AddDatabaseItem(item);
            }
        }
    }
}