using System.Collections.Generic;

namespace ItemManagement
{
    public class ItemDatabase : IItemDatabase
    {
        private readonly Dictionary<string, IItemDefinition> _itemById;

        public ItemDatabase(IEnumerable<IItemDefinition> items)
        {
            _itemById = new Dictionary<string, IItemDefinition>();

            foreach (IItemDefinition item in items)
                AddItem(item);
        }

        public void AddItem(IItemDefinition item)
        {
            if (item == null)
                return;

            if (_itemById.ContainsKey(item.Id))
                return;

            _itemById.Add(item.Id, item);
        }

        public void RemoveItem(string id)
        {
            if (!_itemById.ContainsKey(id))
                return;

            _itemById.Remove(id);
        }

        public IItemDefinition GetItem(string id)
        {
            return _itemById.ContainsKey(id) ? _itemById[id] : null;
        }
    }
}