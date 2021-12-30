using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace InventorySystem
{
    public static class ItemHolder
    {
        private const string ITEMS_PATH = "Items";

        private static readonly Dictionary<string, ItemSO> itemByID = new Dictionary<string, ItemSO>();

        static ItemHolder()
        {
            ItemSO[] items = Resources.LoadAll<ItemSO>(ITEMS_PATH);

            for (int i = 0; i < items.Length; i++)
            {
                if (itemByID.ContainsKey(items[i].ID))
                {
                    Debug.LogWarning($"Item with id: [{items[i].ID}] already registered!");
                    continue;
                }

                itemByID.Add(items[i].ID, items[i]);
            }
        }

        [CanBeNull]
        public static ItemSO GetItem(string id)
        {
            return itemByID.ContainsKey(id) ? itemByID[id] : null;
        }
    }
}