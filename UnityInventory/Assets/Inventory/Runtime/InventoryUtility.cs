using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryUtility
    {
        private const string ITEMS_PATH = "";
        public const int DEFAULT_MAX = -1;

        private static ItemSO _defaultItem;
        private static Dictionary<int, ItemSO> _itemByID = new Dictionary<int, ItemSO>();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            _defaultItem = (ItemSO)ScriptableObject.CreateInstance(typeof(ItemSO));
            _itemByID = new Dictionary<int, ItemSO>();

            ItemSO[] items = Resources.LoadAll<ItemSO>(ITEMS_PATH);

            for (int i = 0; i < items.Length; i++)
            {
                if (_itemByID.ContainsKey(items[i].ID))
                {
                    Debug.LogWarning($"Item with ID: [{items[i].ID}] already registered!", items[i]);
                    continue;
                }

                _itemByID.Add(items[i].ID, items[i]);
            }
        }

        /// <summary>
        /// Retrieve an item from the ID.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="item">The retrieved item.</param>
        /// <returns>True if the item was found.</returns>
        public static bool TryGetItem(int id, out ItemSO item)
        {
            if (_itemByID.ContainsKey(id))
            {
                item = _itemByID[id];
                return true;
            }

            Debug.LogWarning($"Item with ID: [{id}] was not found! Make sure that its located in the Resources folder.");
            item = _defaultItem;
            return false;
        }
    }
}