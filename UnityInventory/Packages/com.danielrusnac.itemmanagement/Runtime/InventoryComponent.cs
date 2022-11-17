using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    public class InventoryComponent : MonoBehaviour, IInventory
    {
        public event Action<ItemChangedData> Changed;

        private Dictionary<int, ISlot> _slots;

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
            return true;
        }

        public bool RemoveSlot(int id)
        {
            if (!ContainsSlot(id))
                return false;

            _slots.Remove(id);
            return true;
        }
    }
}