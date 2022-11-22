using System;
using System.Collections.Generic;
using Items.Inventories;

namespace Items
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        
        IEnumerable<ISlot> Slots { get; }
        
        bool ContainsSlot(IItem item);
        ISlot GetSlotOrCreate(IItem item);
    }
}