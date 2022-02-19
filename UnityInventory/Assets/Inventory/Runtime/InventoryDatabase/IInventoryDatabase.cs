namespace InventorySystem.InventoryDatabase
{
    public interface IInventoryDatabase
    {
        public ItemSO GetItem(int id);
        public bool TryGetItem(int id, out ItemSO item);
    }
}