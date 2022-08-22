using System;
using System.Collections.Generic;
using ItemManagement.Items;
using UnityEngine;

namespace ItemManagement.Database
{
    [CreateAssetMenu(menuName = "Item Management/Databases/Database", fileName = "New Database")]
    public class ItemDatabase : ScriptableObject, IItemDatabase, ISerializationCallbackReceiver
    {
        [SerializeField] private ItemDefinition[] _items;

        private Dictionary<string, IItem> _itemById;

        public IItem GetItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (!_itemById.ContainsKey(id))
                return null;

            return _itemById[id];
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _itemById = CreateDatabaseDictionary(_items);
        }
        
        private Dictionary<string, IItem> CreateDatabaseDictionary(ItemDefinition[] items)
        {
            var itemById = new Dictionary<string, IItem>();
            
            if (items is null || items.Length == 0)
                return itemById;

            foreach (var item in items)
            {
                if (item is null || itemById.ContainsKey(item.Id))
                    continue;

                itemById.Add(item.Id, item);
            }

            return itemById;
        }
    }
}