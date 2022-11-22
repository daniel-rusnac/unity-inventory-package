namespace Items
{
    public interface IItem
    {
        ItemID ID { get; }
        T GetModule<T>(string id) where T : class, IItemModule;
    }
}