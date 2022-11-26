namespace Items
{
    public interface IItemDatabase
    {
        void RegisterItem(IItem item);
        void UnregisterItem(IItem item);
        IItem LoadItem(string id);
    }
}