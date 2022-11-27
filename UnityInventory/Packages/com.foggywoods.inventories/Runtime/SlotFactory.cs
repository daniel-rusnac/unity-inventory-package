namespace Items
{
    public class SlotFactory : ISlotFactory
    {
        public ISlot CreateSlot(IItem item)
        {
            return new Slot(item);
        }
    }
}