using System;

namespace FoggyWoods.Inventories
{
    public interface ISlot
    {
        event Action<ItemChangedData> Changed;
        
        int Limit { get; set; }
        int Amount { get; set; }
        IItem Item { get; }
    }
}