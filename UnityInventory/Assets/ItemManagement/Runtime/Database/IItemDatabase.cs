using ItemManagement.Items;

namespace ItemManagement.Database
{
    public interface IItemDatabase
    {
        IItem GetItem(string id);
    }
}