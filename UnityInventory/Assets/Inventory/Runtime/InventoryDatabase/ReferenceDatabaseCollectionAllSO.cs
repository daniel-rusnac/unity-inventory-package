using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InventorySystem.InventoryDatabase
{
    [CreateAssetMenu(fileName = "Default Collection",
        menuName = InventoryConstants.CREATE_DATABASE_SUB_MENU + "Default Collection")]
    public class ReferenceDatabaseCollectionAllSO : ReferenceDatabaseCollectionSO
    {
        public static Action<ItemSO> AddDatabaseItem = delegate { };
        public static Action<ItemSO> RemoveDatabaseItem = delegate { };

        [SerializeField] private string[] _itemFolders = {"Assets"};

        private void Reset()
        {
            LoadAllItemsFromAssets();
        }

        private void OnEnable()
        {
            AddDatabaseItem += AddItem;
            RemoveDatabaseItem += RemoveItem;
        }

        private void OnDisable()
        {
            AddDatabaseItem -= AddItem;
            RemoveDatabaseItem -= RemoveItem;
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