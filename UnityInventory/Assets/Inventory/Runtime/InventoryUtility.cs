using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryUtility
    {
        private const string ITEMS_PATH = "";
        public const int DEFAULT_MAX = -1;

        private static ItemSO defaultItem;
        private static readonly Dictionary<byte, ItemSO> ItemByID = new Dictionary<byte, ItemSO>();

        static InventoryUtility()
        {
            defaultItem = (ItemSO)ScriptableObject.CreateInstance(typeof(ItemSO));
            
            ItemSO[] items = Resources.LoadAll<ItemSO>(ITEMS_PATH);

            for (int i = 0; i < items.Length; i++)
            {
                if (ItemByID.ContainsKey(items[i].ID))
                {
                    Debug.LogWarning($"Item with ID: [{items[i].ID}] already registered!", items[i]);
                    continue;
                }

                ItemByID.Add(items[i].ID, items[i]);
            }
        }

        public static bool TryGetItem(byte id, out ItemSO item)
        {
            if (ItemByID.ContainsKey(id))
            {
                item = ItemByID[id];
                return true;
            }

            Debug.LogWarning($"Item with ID: [{id}] was not found! Make sure that its located in the Resources folder.");
            item = defaultItem;
            return false;
        }
    }
}