namespace FoggyWoods.Inventories
{
    public interface IItemDatabase
    {
        void RegisterItem(IItem item);
        void UnregisterItem(IItem item);
        IItem LoadItem(string id);
        T LoadItem<T>(string id) where T : IItem;
        bool TryLoadItem<T>(string id, out T item) where T : IItem;
    }
}