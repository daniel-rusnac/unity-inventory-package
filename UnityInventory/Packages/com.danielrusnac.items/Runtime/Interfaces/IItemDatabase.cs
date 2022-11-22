namespace Items
{
    public interface IItemDatabase
    {
        IItem GetItem(ItemID id);
        void AddItem(IItem item);
        void RemoveItem(IItem item);
    }
}