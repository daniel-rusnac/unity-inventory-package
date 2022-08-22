using ItemManagement.Inventories;
using ItemManagement.Items;

namespace ItemManagement.Factories
{
    public class SlotFactory : ISlotFactory
    {
        public ISlot Create(IItem item)
        {
            return new Slot(item);
        }
    }
}