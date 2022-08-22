using ItemManagement.Inventories;
using ItemManagement.Items;

namespace ItemManagement.Factories
{
    public interface ISlotFactory
    {
        ISlot Create(IItem item);
    }
}