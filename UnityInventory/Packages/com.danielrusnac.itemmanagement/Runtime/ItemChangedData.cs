namespace ItemManagement
{
    public struct ItemChangedData
    {
        public readonly IItem Item;
        public readonly IItemDefinition ItemDefinition;
        public readonly int OldAmount;
        public readonly int NewAmount;
        public readonly int Delta;

        public ItemChangedData(IItem item, int oldAmount, int newAmount)
        {
            Item = item;
            ItemDefinition = item.Definition;
            OldAmount = oldAmount;
            NewAmount = newAmount;
            Delta = newAmount - oldAmount;
        }
    }
}