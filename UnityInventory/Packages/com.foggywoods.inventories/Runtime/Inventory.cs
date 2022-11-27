using System;
using System.Collections.Generic;

namespace FoggyWoods.Inventories
{
    public class Inventory : IInventory
    {
        public event Action<ItemChangedData> Changed;
        public event Action<ISlot> SlotAdded;
        public event Action<ISlot> SlotRemoved;

        private readonly ISlotFactory _slotFactory;
        private readonly Dictionary<string, ISlot> _slotByID;

        public IEnumerable<ISlot> Slots => _slotByID.Values;

        public Inventory(ISlotFactory slotFactory)
        {
            _slotFactory = slotFactory;
            _slotByID = new Dictionary<string, ISlot>();
        }

        public bool ContainsSlot(IItem item)
        {
            return _slotByID.ContainsKey(item.ID);
        }

        public ISlot GetOrCreateSlot(IItem item)
        {
            if (!ContainsSlot(item))
                CreateSlot(item);

            return _slotByID[item.ID];
        }

        private void CreateSlot(IItem item)
        {
            ISlot slot = _slotFactory.CreateSlot(item);
            slot.Changed += OnSlotChanged;
            _slotByID.Add(item.ID, slot);
            SlotAdded?.Invoke(slot);
        }

        private void RemoveSlot(IItem item)
        {
            if (!ContainsSlot(item))
                return;

            ISlot slot = _slotByID[item.ID];
            _slotByID.Remove(item.ID);
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