namespace Items
{
    public interface ISlotFactory
    {
        ISlot CreateSlot(IItem item);
    }
}