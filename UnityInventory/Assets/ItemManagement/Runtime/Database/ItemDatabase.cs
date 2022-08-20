using System.Collections.Generic;
using ItemManagement.Items;

namespace ItemManagement.Database
{
    public class ItemDatabase : IItemDatabase
    {
        private readonly Dictionary<string, IItemDefinition> _itemById;

        public ItemDatabase(params IItemDefinition[] items)
        {
            _itemById = new Dictionary<string, IItemDefinition>();
            
            if (items is null || items.Length == 0)
                return;

            foreach (IItemDefinition item in items)
                AddItem(item);
        }

        public void AddItem(IItemDefinition item)
        {
            if (item is null)
                return;

            if (_itemById.ContainsKey(item.Id))
                return;

            _itemById.Add(item.Id, item);
        }

        public void RemoveItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;
            
            if (!_itemById.ContainsKey(id))
                return;

            _itemById.Remove(id);
        }

        public IItemDefinition GetItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (!_itemById.ContainsKey(id))
                return null;
            
            return _itemById[id];
        }
    }
}