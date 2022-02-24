using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Database
{
    [DefaultExecutionOrder(-10)]
    public abstract class SceneDatabase : MonoBehaviour, IInventoryDatabase
    {
        protected readonly Dictionary<int, ItemSO> ItemByID = new Dictionary<int, ItemSO>();
        private bool _isInitialized;

        protected virtual void OnEnable()
        {
            if (OnInitialize())
            {
                InventoryUtility.SetDatabase(this);
                _isInitialized = true;
                return;
            }
            
            Debug.Log("Couldn't initialize database!", this);
        }

        protected virtual void OnDisable()
        {
            if (!_isInitialized)
                return;
            
            InventoryUtility.RemoveDatabase(this);
        }

        public ItemSO GetItem(int id)
        {
            TryGetItem(id, out ItemSO item);
            
            return item;
        }

        public bool TryGetItem(int id, out ItemSO item)
        {
            if (ItemByID.ContainsKey(id))
            {
                item = ItemByID[id];
                return true;
            }

            item = null;
            return false;
        }

        public void AddItem(ItemSO item)
        {
            if (ItemByID.ContainsKey(item.DynamicID))
            {
                Debug.LogWarning($"Item with ID: [{item.DynamicID}] already registered!", item);
                return;
            }

            ItemByID.Add(item.DynamicID, item);
        }

        public void RemoveItem(ItemSO item)
        {
            if (!ItemByID.ContainsKey(item.DynamicID))
                return;

            ItemByID.Remove(item.DynamicID);
        }

        protected abstract bool OnInitialize();
    }
}