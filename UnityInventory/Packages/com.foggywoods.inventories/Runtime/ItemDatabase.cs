using System.Collections.Generic;
using System.Linq;

namespace FoggyWoods.Inventories
{
    public class ItemDatabase : IItemDatabase
    {
        private Dictionary<string, IItem> _itemByID;

        public ItemDatabase()
        {
            _itemByID = new Dictionary<string, IItem>();
        }
        
        public ItemDatabase(params IItem[] items)
        {
            _itemByID = items.ToDictionary(item => item.ID);
        }

        public void RegisterItem(IItem item)
        {
            _itemByID.Add(item.ID, item);
        }

        public void UnregisterItem(IItem item)
        {
            _itemByID.Remove(item.ID);
        }

        public IItem LoadItem(string id)
        {
            if (_itemByID.ContainsKey(id))
                return _itemByID[id];

            return default;
        }

        public T LoadItem<T>(string id) where T : IItem
        {
            if (_itemByID.ContainsKey(id))
                return (T) _itemByID[id];

            return default;
        }

        public bool TryLoadItem<T>(string id, out T item) where T : IItem
        {
            if (_itemByID.ContainsKey(id) && _itemByID[id] is T result)
            {
                item = result;
                return true;
            }

            item = default;
            return false;
        }
    }
}