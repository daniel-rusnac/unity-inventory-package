using Items.Inventories;

namespace Items.Factories
{
    public interface ISlotFactory
    {
        ISlot Create(IItem item);
    }
}