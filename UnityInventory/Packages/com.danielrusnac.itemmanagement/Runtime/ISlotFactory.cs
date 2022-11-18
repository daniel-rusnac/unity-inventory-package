namespace ItemManagement
{
    public interface ISlotFactory
    {
        ISlot Create(IItem item);
    }
}