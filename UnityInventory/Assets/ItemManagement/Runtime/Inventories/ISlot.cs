using System;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public interface ISlot
    {
        event Action<IItem, int> Changed;

        int Amount { get; }
        IItem Item { get; }

        void Add(int amount);
        void Remove(int amount);
        void Clear();
    }
}