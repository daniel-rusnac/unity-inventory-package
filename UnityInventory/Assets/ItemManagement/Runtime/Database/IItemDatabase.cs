using ItemManagement.Items;

namespace ItemManagement.Database
{
    public interface IItemDatabase
    {
        IItemDefinition GetItem(string id);
    }
}