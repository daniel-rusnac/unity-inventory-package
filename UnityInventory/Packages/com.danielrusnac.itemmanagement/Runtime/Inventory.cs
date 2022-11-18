using System;
using System.Collections.Generic;

namespace Items
{
    public class Inventory : IInventory
    {
        public event Action<ItemChangedData> Changed;
        public event Action<ISlot> SlotAdded;
        public event Action<ISlot> SlotRemoved;

        private Dictionary<ItemID, ISlot> _slots;
        private ISlotFactory _slotFactory;

        public Inventory(ISlotFactory slotFactory)
        {
            _slotFactory = slotFactory;
        }

        public IEnumerable<ISlot> GetAllSlots()
        {
            return _slots.Values;
        }

        public bool ContainsSlot(IItem item)
        {
            return _slots.ContainsKey(item.ID);
        }

        public ISlot GetSlotOrCreate(IItem item)
        {
            if (!ContainsSlot(item))
                CreateSlot(item);

            return _slots[item.ID];
        }

        private void CreateSlot(IItem item)
        {
            ISlot slot = _slotFactory.Create(item);
            slot.Changed += OnSlotChanged;
            _slots.Add(item.ID, slot);
            SlotAdded?.Invoke(slot);
        }

        private void RemoveSlot(IItem item)
        {
            ISlot slot = _slots[item.ID];
            _slots.Remove(item.ID);
            SlotRemoved?.Invoke(slot);
        }

        private void OnSlotChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);

            if (data.NewAmount == 0)
                RemoveSlot(data.Item);
        }
    }
}