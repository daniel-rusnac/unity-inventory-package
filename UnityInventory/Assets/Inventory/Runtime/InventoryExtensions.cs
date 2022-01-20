namespace InventorySystem
{
    public static class InventoryExtensions
    {
        public static void TransferTo(this Inventory from, Inventory to, ItemSO item, int amount)
        {
            TransferTo(from, to, item.ID, amount);
        }
        
        public static void TransferTo(this Inventory from, Inventory to, byte itemID, int amount)
        {
            int itemsRemoved = from.GetCount(itemID);
            from.Remove(itemID, amount);
            to.Add(itemID, itemsRemoved);
        }
    }
}