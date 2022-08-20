namespace ItemManagement.Items
{
    public interface IItem
    {
        string Id { get; }
        bool IsStackable { get; }
    }
}