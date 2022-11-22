using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Inventories
{
    public abstract class InventorySO<T> : ScriptableObject, IInventory where T : IInventory
    {
        public event Action<ItemChangedData> Changed;
        public IEnumerable<ISlot> Slots => Inventory.Slots;

        protected abstract T Inventory { get; }

        protected virtual void OnEnable()
        {
            Inventory.Changed += OnChanged;
        }

        protected virtual void OnDisable()
        {
            Inventory.Changed -= OnChanged;
        }

        public bool ContainsSlot(IItem item) => Inventory.ContainsSlot(item);

        public ISlot GetSlotOrCreate(IItem item) => Inventory.GetSlotOrCreate(item);

        private void OnChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);
        }
    }
}