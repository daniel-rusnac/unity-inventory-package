using InventorySystem.InventoryDatabase;
using JetBrains.Annotations;

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
        [CanBeNull]
        public static ItemSO GetItem(int id)
        {
            return _database.GetItem(id);
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