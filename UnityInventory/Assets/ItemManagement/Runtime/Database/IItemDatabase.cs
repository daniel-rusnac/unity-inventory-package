using ItemManagement.Items;

namespace ItemManagement.Database
{
    public interface IItemDatabase
    {
        void AddItem(IItemDefinition item);
        void RemoveItem(string id);
        IItemDefinition GetItem(string id);
    }
}