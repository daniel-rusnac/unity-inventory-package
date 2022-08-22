using System;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public interface IInventory
    {
        event Action<IItem, int> Changed;
        
        int GetAmount(IItem item);
        void Add(IItem item, int amount);
        void Remove(IItem item, int amount);
        void Clear();
    }
}