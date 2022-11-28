using System;
using System.Collections.Generic;

namespace FoggyWoods.Inventories
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;
        event Action<ISlot> SlotAdded;
        event Action<ISlot> SlotRemoved;
        
        IEnumerable<ISlot> Slots { get; }

        bool ContainsSlot(IItem item);
        ISlot GetOrCreateSlot(IItem item);

        InventoryData Serialize();
        void Deserialize(InventoryData data, IItemDatabase database);
    }
}