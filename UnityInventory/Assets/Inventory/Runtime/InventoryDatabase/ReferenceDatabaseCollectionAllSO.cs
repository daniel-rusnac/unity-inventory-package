using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InventorySystem.InventoryDatabase
{
    [CreateAssetMenu(fileName = "Default Collection", menuName = InventoryConstants.CREATE_DATABASE_SUB_MENU + "Default Collection")]
    public class ReferenceDatabaseCollectionAllSO : ReferenceDatabaseCollectionSO
    {
        [SerializeField] private string[] _itemFolders = {"Assets"};
        
        private void Reset()
        {
            LoadAllItemsFromAssets();
        }

        [ContextMenu("Load All Items From Assets")]
        private void LoadAllItemsFromAssets()
        {
#if UNITY_EDITOR
            if (_itemFolders.Length == 0)
            {
                Debug.LogWarning("No folders to load items from!");
                return;
            }
            
            _items = AssetDatabase.FindAssets($"t:{nameof(ItemSO)}", _itemFolders)
                .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                .Cast<ItemSO>().ToArray();

            EditorUtility.SetDirty(this);
#endif
        }
    }
}