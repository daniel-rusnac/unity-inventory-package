using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class ResourcesDatabase : IInventoryDatabase
    {
        private readonly Dictionary<int, ItemSO> _itemByID;

        public ResourcesDatabase()
        {
            _itemByID = new Dictionary<int, ItemSO>();
            ItemSO[] items = Resources.LoadAll<ItemSO>("");
            
            foreach (ItemSO t in items)
            {
                if (_itemByID.ContainsKey(t.ID))
                {
                    Debug.LogWarning($"Item with ID: [{t.ID}] already registered!", t);
                    continue;
                }
                
                _itemByID.Add(t.ID, t);
            }
        }

        public ItemSO GetItem(int id)
        {
            TryGetItem(id, out ItemSO item);
            
            return item;
        }

        public bool TryGetItem(int id, out ItemSO item)
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