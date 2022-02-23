namespace InventorySystem
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Check is the inventory has at least on item.
        /// </summary>
        public static bool IsUnlocked(this ItemSO item, OldInventorySO inventory)
        {
            return inventory.Contains(item);
        }
    }
}