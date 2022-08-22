using System;
using System.Collections.Generic;
using ItemManagement.Factories;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public class Inventory : IInventory
    {
        public event Action<IItem, int> Changed;

        private readonly Dictionary<string, ISlot> _slotByID;
        private readonly ISlotFactory _slotFactory;

        public Inventory(ISlotFactory slotFactory)
        {
            _slotFactory = slotFactory;
            _slotByID = new Dictionary<string, ISlot>();
        }

        public int GetAmount(IItem item)
        {
            if (_slotByID.ContainsKey(item.Id))
                return _slotByID[item.Id].Amount;

            return 0;
        }

        public void Add(IItem item, int amount)
        {
            if (_slotByID.ContainsKey(item.Id))
                _slotByID.Add(item.Id, CreateSlot(item));

            _slotByID[item.Id].Add(amount);
        }

        public void Remove(IItem item, int amount)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Remove(amount);

            if (_slotByID[item.Id].Amount <= 0)
                RemoveSlot(item);
        }

        public void Clear()
        {
            foreach (ISlot slot in _slotByID.Values)
                slot.Clear();

            _slotByID.Clear();
        }

        private ISlot CreateSlot(IItem item)
        {
            ISlot slot = _slotFactory.Create(item);
            slot.Changed += OnSlotChanged;
            return slot;
        }

        private void RemoveSlot(IItem item)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Changed -= OnSlotChanged;
            _slotByID.Remove(item.Id);
        }

        private void OnSlotChanged(IItem item, int delta)
        {
            Changed?.Invoke(item, delta);
        }
    }
}