using System;
using System.Collections.Generic;
using System.Linq;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public class Inventory : IInventory
    {
        public event Action<IItem, int> Changed;

        private readonly Dictionary<string, ISlot> _slotByID;

        public Inventory()
        {
            _slotByID = new Dictionary<string, ISlot>();
        }
        
        public ISlot[] GetSlots()
        {
            return _slotByID.Values.ToArray();
        }

        public void Add(IItem item, int amount)
        {
            if (_slotByID.ContainsKey(item.Id))
                _slotByID.Add(item.Id, CreateSlot(item));

            _slotByID[item.Id].Amount += amount;
        }

        public void Remove(IItem item, int amount)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Amount -= amount;

            if (_slotByID[item.Id].Amount <= 0)
                RemoveSlot(item);
        }

        public void Clear()
        {
            foreach (ISlot slot in _slotByID.Values)
                slot.Amount = 0;
            
            _slotByID.Clear();
        }

        private ISlot CreateSlot(IItem item)
        {
            ISlot slot = null;
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