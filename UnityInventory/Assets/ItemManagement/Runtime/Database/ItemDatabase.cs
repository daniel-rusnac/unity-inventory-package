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

        private void AddItem(IItem item)
        {
            if (item is null)
                return;

            if (_itemById.ContainsKey(item.Id))
                return;

            _itemById.Add(item.Id, item);
        }

        private Dictionary<string, IItem> CreateDatabaseDictionary(ItemDefinition[] items)
        {
            if (items is null || items.Length == 0)
                return new Dictionary<string, IItem>();

            var itemById = new Dictionary<string, IItem>();
            
            foreach (var item in items)
                AddItem(item);

            return itemById;
        }
    }
}