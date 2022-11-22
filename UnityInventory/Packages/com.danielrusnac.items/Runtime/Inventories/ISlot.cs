using System;

namespace Items.Inventories
{
    public interface ISlot
    {
        event Action<ItemChangedData> Changed;
        
        ItemID ID { get; }
        IItem Item { get; }
        int Amount { get; set; }
        int Limit { get; set; }
    }
}