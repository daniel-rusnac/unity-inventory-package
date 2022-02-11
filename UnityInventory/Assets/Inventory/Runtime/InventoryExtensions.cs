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
            TransferTo(from, to, item.ID, amount);
        }
        
        /// <summary>
        /// Removed the items from an inventory and ads them to another.
        /// </summary>
        /// <param name="from">Inventory to remove items from.</param>
        /// <param name="to">Inventory to add items to.</param>
        /// <param name="itemID">The ID of the item to transfer.</param>
        /// <param name="amount">The amount to transfer. Will not add more items than removed.</param>
        public static void TransferTo(this InventorySO from, InventorySO to, int itemID, int amount)
        {
            int itemsRemoved = from.GetCount(itemID);
            from.Remove(itemID, amount);
            to.Add(itemID, itemsRemoved);
        }
    }
}