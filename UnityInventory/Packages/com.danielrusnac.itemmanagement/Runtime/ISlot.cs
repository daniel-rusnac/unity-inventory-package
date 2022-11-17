using System;

namespace ItemManagement
{
    public interface ISlot
    {
        event Action<ItemChangedData> Changed;
        int Amount { get; set; }
        int MaxAmount { get; set; }
        IItemDefinition ItemDefinition { get; }
        IItem Item { get; }
    }
}