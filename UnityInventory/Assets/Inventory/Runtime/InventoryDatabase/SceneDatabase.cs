using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public abstract class SceneDatabase : MonoBehaviour, IInventoryDatabase
    {
        protected readonly Dictionary<int, ItemSO> ItemByID = new Dictionary<int, ItemSO>();
        private bool _isInitialized;

        protected virtual void Awake()
        {
            if (OnInitialize())
            {
                InventoryUtility.SetDatabase(this);
                _isInitialized = true;
                return;
            }
            
            Debug.Log("Couldn't initialize database!", this);
        }

        protected virtual void OnDestroy()
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

        protected abstract bool OnInitialize();
    }
}