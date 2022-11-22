namespace Items
{
    public interface IItem
    {
        ItemID ID { get; }
        string Name { get; }
        T GetModule<T>(string id) where T : class, IItemModule;
    }
}