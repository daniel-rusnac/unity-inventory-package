namespace InventorySystem.Slots
{
    public abstract class Slot
    {
        public bool ShouldBeRemoved => Amount <= 0;
        public abstract int StaticID { get; }
        public abstract int DynamicID { get; }
        public abstract long Amount { get; set; }
    }
}