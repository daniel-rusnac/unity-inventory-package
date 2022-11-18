﻿using System;

namespace Items
{
    public interface ISlot
    {
        event Action<ItemChangedData> Changed;
        
        ItemID ID { get; }
        IItem Item { get; }
        int Amount { get; set; }
    }
}