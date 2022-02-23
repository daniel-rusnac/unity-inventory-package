namespace InventorySystem.Slots
{
    public abstract class Slot
    {
        public abstract int StaticID { get; }
        public abstract int DynamicID { get; }
        public abstract long Amount { get; }
        public abstract long Limit { get; }
        
        public abstract void Add(long amount);
        public abstract void Remove(long amount);
        public abstract void SetLimit(long limit);
    }
}