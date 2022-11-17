using System;
using System.Collections.Generic;

namespace ItemManagement
{
    public interface IInventory
    {
        event Action<ItemChangedData> ItemChanged;
        event Action<ISlot> SlotAdded;
        event Action<ISlot> SlotRemoved;

        IEnumerable<ISlot> AllSlots { get; }
        bool ContainsSlot(int id);
        ISlot GetSlot(int id);
        bool AddSlot(int id, ISlot slot);
        bool RemoveSlot(int id);
    }
}