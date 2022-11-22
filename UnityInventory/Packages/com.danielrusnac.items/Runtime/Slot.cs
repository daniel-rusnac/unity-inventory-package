using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class Slot
    {
        public event Action<ItemChangedData> Changed;

        private int _amount;
        private int _limit;

        public int ID { get; }

        public Item Item { get; }

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

        public Slot()
        {
            _limit = -1;
        }

        public Slot(Item item, int amount = 0) : this()
        {
            ID = item.ID;
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