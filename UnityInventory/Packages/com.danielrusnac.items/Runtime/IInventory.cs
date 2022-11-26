using System;
using System.Collections.Generic;

namespace Items
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        
        IReadOnlyList<ISlot> Slots { get; }

        bool ContainsSlot(IItem item);
        bool GetSlotOrCreate(IItem item);
    }
}