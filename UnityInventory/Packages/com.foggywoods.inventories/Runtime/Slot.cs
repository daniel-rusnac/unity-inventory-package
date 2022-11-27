using System;
using UnityEngine;

namespace FoggyWoods.Inventories
{
    public class Slot : ISlot
    {
        public event Action<ItemChangedData> Changed;

        private int _amount;
        private int _limit;

        public IItem Item { get; }

        public int Amount
        {
            get => _amount;
            set => SetAmount(value);
        }

        public int Limit
        {
            get => _limit;
            set => SetLimit(value);
        }

        public Slot(IItem item, int amount = 0)
        {
            Item = item;
            _amount = amount;
            _limit = int.MaxValue;
        }

        private void SetAmount(int value)
        {
            if (_amount == value)
                return;

            int oldAmount = _amount;
            _amount = ClampAmount(value);
            Changed?.Invoke(new ItemChangedData(Item, oldAmount, _amount));
        }

        private void SetLimit(int value)
        {
            if (_limit == value)
                return;

            _limit = value;
            Amount = ClampAmount(_amount);
        }

        private int ClampAmount(int value)
        {
            return Mathf.Clamp(value, 0, Limit);
        }
    }
}