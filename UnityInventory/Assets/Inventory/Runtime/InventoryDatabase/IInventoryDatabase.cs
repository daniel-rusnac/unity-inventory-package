namespace InventorySystem.InventoryDatabase
{
    public interface IInventoryDatabase
    {
        public IItem GetItem(int id);
        public bool TryGetItem(int id, out IItem item);
    }
}