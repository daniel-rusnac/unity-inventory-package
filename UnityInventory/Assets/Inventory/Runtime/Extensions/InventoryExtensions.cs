using UnityEngine;

namespace InventorySystem
{
    public static class InventoryExtensions
    {
        /// <summary>
        /// Removed the items from an inventory and ads them to another.
        /// </summary>
        /// <param name="from">Inventory to remove items from.</param>
        /// <param name="to">Inventory to add items to.</param>
        /// <param name="item">The item to transfer.</param>
        /// <param name="amount">The amount to transfer. Will not add more items than removed.</param>
        public static void TransferTo(this InventorySO from, InventorySO to, ItemSO item, int amount)
        {
            int itemsRemoved = from.GetAmount(item);
            from.Remove(item, amount);
            to.Add(item, itemsRemoved);
        }

        /// <summary>
        /// Save the inventory using PlayerPrefs
        /// </summary>
        public static void Save(this InventorySO inventory, string saveKey)
        {
            PlayerPrefs.SetString(saveKey, inventory.Serialize());
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Load the inventory using PlayerPrefs
        /// </summary>
        public static void Load(this InventorySO inventory, string saveKey)
        {
            inventory.Deserialize(PlayerPrefs.GetString(saveKey));
        }
    }
}