using System;

namespace ItemManagement
{
    public interface ISlot
    {
        event Action<ItemChangedData> Changed;
        
        ItemID ID { get; }
        IItem Item { get; }
        int Amount { get; set; }
    }
}