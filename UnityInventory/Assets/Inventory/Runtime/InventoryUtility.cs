using System.Collections.Generic;
using InventorySystem.InventoryDatabase;
using UnityEditor.Callbacks;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryUtility
    {
        internal const int DEFAULT_MAX = -1;

        private static readonly IInventoryDatabase _database;

        static InventoryUtility()
        {
            _database = new ResourcesDatabase();
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="item">The retrieved item.</param>
        /// <returns>True if the item was found.</returns>
        public static bool TryGetItem(int id, out ItemSO item)
        {
            return _database.TryGetItem(id, out item);
        }
    }
}