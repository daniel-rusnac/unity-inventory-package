using System;
using UnityEngine;

namespace Items
{
    public class Slot : ISlot
    {
        public event Action<ItemChangedData> Changed;

        private int _amount;

        public ItemID ID { get; }

        public IItem Item { get; }

        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount == value)
                    return;

                int oldAmount = _amount;
                _amount = Mathf.Max(value, 0);
                Changed?.Invoke(new ItemChangedData(Item, oldAmount, _amount));
            }
        }

        public Slot() { }

        public Slot(ItemID id, IItem item, int amount = 0)
        {
            ID = id;
            Item = item;
            _amount = amount;
        }
    }
}