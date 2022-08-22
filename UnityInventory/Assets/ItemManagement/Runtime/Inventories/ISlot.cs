using System;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public interface ISlot
    {
        event Action<IItem, int> Changed;

        int Amount { get; set; }
        IItem Item { get; }
    }
}