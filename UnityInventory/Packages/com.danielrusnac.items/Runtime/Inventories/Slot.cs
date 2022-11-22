using System;
using UnityEngine;

namespace Items.Inventories
{
    public class Slot : ISlot
    {
        public event Action<ItemChangedData> Changed;

        private int _amount;
        private int _limit;

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
                _amount = ClampAmount(value);
                Changed?.Invoke(new ItemChangedData(Item, oldAmount, _amount));
            }
        }

        public int Limit
        {
            get => _limit;
            set
            {
                if (_limit == value)
                    return;
                
                _limit = value;
                OnLimitChanged();
            }
        }

        public Slot() { }

        public Slot(ItemID id, IItem item, int amount = 0)
        {
            ID = id;
            Item = item;
            _amount = amount;
        }

        private void OnLimitChanged()
        {
            Amount = ClampAmount(_amount);
        }

        private int ClampAmount(int value)
        {
            value = Mathf.Max(value, 0);
            
            if (Limit >= 0)
                value = Mathf.Min(value, Limit);
            
            return value;
        }
    }
}