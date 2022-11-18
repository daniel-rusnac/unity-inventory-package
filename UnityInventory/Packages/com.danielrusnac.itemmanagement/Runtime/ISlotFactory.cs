namespace Items
{
    public interface ISlotFactory
    {
        ISlot Create(IItem item);
    }
}