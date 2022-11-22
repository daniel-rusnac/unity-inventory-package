using System;
using System.Collections.Generic;
using Items.Factories;

namespace Items.Inventories
{
    public class Inventory : IInventory
    {
        public event Action<ItemChangedData> Changed;

        private readonly Dictionary<ItemID, ISlot> _slots;
        private readonly ISlotFactory _slotFactory;

        public IEnumerable<ISlot> Slots => _slots.Values;

        public Inventory(ISlotFactory slotFactory)
        {
            _slots = new Dictionary<ItemID, ISlot>();
            _slotFactory = slotFactory;
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
        }

        private void RemoveSlot(IItem item)
        {
            ISlot slot = _slots[item.ID];
            _slots.Remove(item.ID);
        }

        private void OnSlotChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);

            if (data.NewAmount == 0)
                RemoveSlot(data.Item);
        }
    }
}