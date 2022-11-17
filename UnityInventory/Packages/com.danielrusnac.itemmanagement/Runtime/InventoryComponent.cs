using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    public class InventoryComponent : MonoBehaviour, IInventory
    {
        public event Action<ItemChangedData> ItemChanged;
        public event Action<ISlot> SlotAdded;
        public event Action<ISlot> SlotRemoved;

        private Dictionary<int, ISlot> _slots;

        public IEnumerable<ISlot> AllSlots => _slots.Values;

        private void Awake()
        {
            _slots = new Dictionary<int, ISlot>();
        }

        public bool ContainsSlot(int id)
        {
            return _slots.ContainsKey(id);
        }

        public ISlot GetSlot(int id)
        {
            if (!ContainsSlot(id))
                return default;

            return _slots[id];
        }

        public bool AddSlot(int id, ISlot slot)
        {
            if (ContainsSlot(id))
                return false;

            _slots.Add(id, slot);
            slot.Changed += OnItemChanged;
            SlotAdded?.Invoke(slot);
            return true;
        }

        public bool RemoveSlot(int id)
        {
            if (!ContainsSlot(id))
                return false;

            ISlot slot = _slots[id];
            _slots.Remove(id);
            slot.Changed += OnItemChanged;
            SlotRemoved?.Invoke(slot);
            
            return true;
        }

        private void OnItemChanged(ItemChangedData data)
        {
            ItemChanged?.Invoke(data);
        }
    }
}