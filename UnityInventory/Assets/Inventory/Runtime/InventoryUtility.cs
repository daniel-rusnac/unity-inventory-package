using System;
using InventorySystem.InventoryDatabase;
using JetBrains.Annotations;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryUtility
    {
        internal const long DEFAULT_LIMIT = -1;

        private static IInventoryDatabase s_database;
        
        public static bool IsInitialized { get; private set; }

        public static void SetDatabase(IInventoryDatabase database)
        {
            s_database = database;
            IsInitialized = true;
        }

        public static void RemoveDatabase(IInventoryDatabase database)
        {
            if (s_database != database)
                return;
            
            RemoveDatabase();
        }

        public static void RemoveDatabase()
        {
            s_database = null;
            IsInitialized = false;
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        [CanBeNull]
        public static ItemSO GetItem(int id)
        {
            if (!IsInitialized)
            {
                if (Application.isPlaying)
                {
                    Debug.LogWarning("Inventory database is not initialized!");
                }
                return null;
            }
            
            return s_database.GetItem(id);
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="item">The retrieved item.</param>
        /// <returns>True if the item was found.</returns>
        public static bool TryGetItem(int id, out ItemSO item)
        {
            if (!IsInitialized)
            {
                if (Application.isPlaying)
                {
                    Debug.LogWarning("Inventory database is not initialized!");
                }
                
                item = null;
                return false;
            }
            
            return s_database.TryGetItem(id, out item);
        }

        public static void AddItemToDatabase(ItemSO item)
        {
            if (!IsInitialized)
                return;
            
            s_database.AddItem(item);
        }
        
        public static void RemoveItemToDatabase(ItemSO item)
        {
            if (!IsInitialized)
                return;
            
            s_database.RemoveItem(item);
        }
        
        public static int GetID()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}