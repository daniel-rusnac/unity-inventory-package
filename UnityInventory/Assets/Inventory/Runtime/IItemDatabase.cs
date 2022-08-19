namespace ItemManagement
{
    public interface IItemDatabase
    {
        IItem LoadItem(string id);
    }
}