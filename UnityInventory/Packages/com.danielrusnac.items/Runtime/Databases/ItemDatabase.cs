using System.Collections.Generic;
using System.Linq;

namespace Items.Databases
{
    public class ItemDatabase : IItemDatabase
    {
        private readonly Dictionary<ItemID, IItem> _items;

        public ItemDatabase()
        {
            _items = new Dictionary<ItemID, IItem>();
        }

        public ItemDatabase(params IItem[] items)
        {
            _items = items.ToDictionary(item => item.ID);
        }

        public IItem GetItem(ItemID id)
        {
            return _items.ContainsKey(id)
                ? _items[id]
                : default;
        }

        public void AddItem(IItem item)
        {
            _items.Add(item.ID, item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item.ID);
        }
    }
}