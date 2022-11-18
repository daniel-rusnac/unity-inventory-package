using System;
using System.Collections.Generic;

namespace Items.Inventories
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        event Action<ISlot> SlotAdded;
        event Action<ISlot> SlotRemoved;
        
        IEnumerable<ISlot> GetAllSlots();
        bool ContainsSlot(IItem item);
        ISlot GetSlotOrCreate(IItem item);
    }
}