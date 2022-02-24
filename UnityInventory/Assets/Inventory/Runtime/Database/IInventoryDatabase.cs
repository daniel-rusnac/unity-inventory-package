namespace InventorySystem.Database
{
    public interface IInventoryDatabase
    {
        public ItemSO GetItem(int id);
        public bool TryGetItem(int id, out ItemSO item);
        public void AddItem(ItemSO item);
        public void RemoveItem(ItemSO item);
    }
}