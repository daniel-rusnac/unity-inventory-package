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

                if (!t.IsAssignableFrom(typeof(ItemSO)) && !t.IsSubclassOf(typeof(ItemSO))) 
                    continue;
                
                ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>(str);
                ReferenceDatabaseCollectionAllSO.AddDatabaseItem(item);
            }

            if (deleted.Length > 0)
            {
                ReferenceDatabaseCollectionAllSO.RefreshDatabase();
            }
        }
    }
}