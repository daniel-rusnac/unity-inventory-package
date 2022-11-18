using System;
using System.Collections.Generic;

namespace ItemManagement
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        event Action<ISlot> SlotAdded;
        event Action<ISlot> SlotRemoved;
        
        IEnumerable<ISlot> GetAllSlots();
        ISlot GetSlotOrCreate(IItem item);
    }
}