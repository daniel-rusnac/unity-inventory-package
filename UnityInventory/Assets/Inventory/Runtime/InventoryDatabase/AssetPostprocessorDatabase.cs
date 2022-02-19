using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class AssetPostprocessorDatabase : IInventoryDatabase
    {
        public static Action<ItemSO> AddDatabaseItem = delegate {  };
        public static Action<ItemSO> RemoveDatabaseItem = delegate {  };
        
        private readonly Dictionary<int, ItemSO> _itemByID;

        public AssetPostprocessorDatabase()
        {
            AddDatabaseItem += AddItem;
            RemoveDatabaseItem += RemoveItem;

            _itemByID = new Dictionary<int, ItemSO>();
        }

        ~AssetPostprocessorDatabase()
        {
            AddDatabaseItem -= AddItem;
            RemoveDatabaseItem -= RemoveItem;
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

        private void AddItem(ItemSO item)
        {
            Debug.Log("A");
            if (_itemByID.ContainsKey(item.ID))
                return;
            
            _itemByID.Add(item.ID, item);
            Debug.Log("Add" + item.name);
        }

        private void RemoveItem(ItemSO item)
        {
            Debug.Log("B");
            if (!_itemByID.ContainsKey(item.ID))
                return;
            
            _itemByID.Remove(item.ID);
            Debug.Log("Remove " + item.name);
        }
    }
}