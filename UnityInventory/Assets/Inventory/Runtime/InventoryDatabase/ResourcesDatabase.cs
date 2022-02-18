using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class ResourcesDatabase : IInventoryDatabase
    {
        private readonly Dictionary<int, IItem> _itemByID;

        public ResourcesDatabase()
        {
            _itemByID = new Dictionary<int, IItem>();
            IItem[] items = Resources.LoadAll<ScriptableObject>("Items").Cast<IItem>().Where(o => o != null).ToArray();
            
            foreach (IItem t in items)
            {
                if (_itemByID.ContainsKey(t.ID))
                {
                    Debug.LogWarning($"Item with ID: [{t.ID}] already registered!");
                    continue;
                }
                
                _itemByID.Add(t.ID, t);
            }
        }

        public IItem GetItem(int id)
        {
            TryGetItem(id, out IItem item);
            
            return item;
        }

        public bool TryGetItem(int id, out IItem item)
        {
            if (_itemByID.ContainsKey(id))
            {
                item = _itemByID[id];
                return true;
            }

            item = null;
            return false;
        }
    }
}