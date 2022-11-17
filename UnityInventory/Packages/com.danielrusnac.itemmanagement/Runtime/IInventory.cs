using System;

namespace ItemManagement
{
    public interface IInventory
    {
        event Action<ItemChangedData> Changed;

        bool ContainsSlot(int id);
        ISlot GetSlot(int id);
        bool AddSlot(int id, ISlot slot);
        bool RemoveSlot(int id);
    }
}