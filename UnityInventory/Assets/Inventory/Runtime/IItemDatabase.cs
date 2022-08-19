namespace InventorySystem
{
    public interface IItemDatabase
    {
        IItem LoadItem(string id);
    }
}