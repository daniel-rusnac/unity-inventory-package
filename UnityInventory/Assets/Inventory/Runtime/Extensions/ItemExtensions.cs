namespace InventorySystem
{
    public static class ItemExtensions
    {
        public static bool IsDynamic(this ItemSO item)
        {
            return item is IDynamicItemBase;
        }
    }
}