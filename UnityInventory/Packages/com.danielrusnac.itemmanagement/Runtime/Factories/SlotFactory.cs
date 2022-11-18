using Items.Inventories;

namespace Items.Factories
{
    public class SlotFactory : ISlotFactory
    {
        public ISlot Create(IItem item)
        {
            return new Slot(item.ID, item);
        }
    }
}