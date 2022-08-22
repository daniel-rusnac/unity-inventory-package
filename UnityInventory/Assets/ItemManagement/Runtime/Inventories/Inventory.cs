using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public IItem[] GetItems()
        {
            return _slotByID.Values.Select(slot => slot.Item).ToArray();
        }

        public ISlot[] GetSlots()
        {
            return _slotByID.Values.ToArray();
        }

        public int GetAmount(IItem item)
        {
            return _slotByID.ContainsKey(item.Id) 
                ? _slotByID[item.Id].Amount 
                : 0;
        }

        public void Add(IItem item, int amount)
        {
            GetOrCreateSlot(item).Add(amount);
        }

        public void Remove(IItem item, int amount)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Remove(amount);
        }

        public void Clear()
        {
            foreach (ISlot slot in _slotByID.Values)
                slot.Clear();

            _slotByID.Clear();
        }

        public ISlot GetOrCreateSlot(IItem item)
        {
            if (_slotByID.ContainsKey(item.Id))
                return _slotByID[item.Id];

            var slot = _slotFactory.Create(item);
            _slotByID.Add(item.Id, slot);
            slot.Changed += OnSlotChanged;
            return slot;
        }

        public void RemoveSlot(IItem item)
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