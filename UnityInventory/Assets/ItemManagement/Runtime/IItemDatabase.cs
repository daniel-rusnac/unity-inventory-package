namespace ItemManagement
{
    public interface IItemDatabase
    {
        IItem GetItem(string id);
    }
}