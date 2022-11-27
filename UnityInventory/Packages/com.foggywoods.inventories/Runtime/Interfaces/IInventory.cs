using System;
using System.Collections.Generic;

namespace Items
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        
        IEnumerable<ISlot> Slots { get; }

        bool ContainsSlot(IItem item);
        ISlot GetOrCreateSlot(IItem item);
    }
}