namespace FoggyWoods.Inventories
{
    public interface ISlotFactory
    {
        ISlot CreateSlot(IItem item);
    }
}