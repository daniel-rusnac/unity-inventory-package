using System;
using Items.Inventories;

namespace Items
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