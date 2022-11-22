namespace Items
{
    public interface IItem
    {
        ItemID ID { get; }
        string Name { get; }
    }
}