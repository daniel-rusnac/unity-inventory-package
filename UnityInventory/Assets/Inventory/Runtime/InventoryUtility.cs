using InventorySystem.InventoryDatabase;
using JetBrains.Annotations;
#if UNITY_EDITOR
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryUtility
    {
        internal const long DEFAULT_LIMIT = -1;

        private static readonly IInventoryDatabase _database;

        static InventoryUtility()
        {
            _database = new ResourcesDatabase();
        }

#if UNITY_EDITOR
        [DidReloadScripts]
#endif
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            // a dummy method to trigger database initialization
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        [CanBeNull]
        public static IItem GetItem(int id)
        {
            return _database.GetItem(id);
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="item">The retrieved item.</param>
        /// <returns>True if the item was found.</returns>
        public static bool TryGetItem(int id, out IItem item)
        {
            return _database.TryGetItem(id, out item);
        }
    }
}