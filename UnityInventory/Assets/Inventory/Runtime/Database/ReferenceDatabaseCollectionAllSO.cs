using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace InventorySystem.Database
{
    [CreateAssetMenu(fileName = "Default Collection",
        menuName = InventoryConstants.CREATE_DATABASE_SUB_MENU + "Default Collection")]
    public class ReferenceDatabaseCollectionAllSO : ReferenceDatabaseCollectionSO
    {
        public static Action<ItemSO> AddDatabaseItem = delegate { };
        public static Action<ItemSO> RemoveDatabaseItem = delegate { };
        public static Action RefreshDatabase = delegate { };

        [SerializeField] private string[] _itemFolders = {"Assets"};

        private void Reset()
        {
            LoadAllItemsFromAssets();
        }

        private void OnEnable()
        {
            AddDatabaseItem += AddItem;
            RemoveDatabaseItem += RemoveItem;
            RefreshDatabase += Refresh;
        }

        private void OnDisable()
        {
            AddDatabaseItem -= AddItem;
            RemoveDatabaseItem -= RemoveItem;
            RefreshDatabase -= Refresh;
        }

        private void AddItem(ItemSO item)
        {
            List<ItemSO> items = _items.ToList();

            if (items.Contains(item))
                return;

            items.Add(item);

            _items = items.ToArray();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private void RemoveItem(ItemSO item)
        {
            List<ItemSO> items = _items.ToList();

            if (!items.Contains(item))
                return;

            items.Remove(item);

            _items = items.ToArray();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private void Refresh()
        {
            _items = _items.Where(so => so != null).ToArray();
            
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
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
                .Select(guid => AssetDatabase.LoadAssetAtPath<ItemSO>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();

            EditorUtility.SetDirty(this);
#endif
        }
    }
}