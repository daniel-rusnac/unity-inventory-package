using System;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public class Slot : ISlot
    {
        public event Action<IItem, int> Changed;

        public int Amount { get; private set; }

        public IItem Item { get; }

        public Slot(IItem item)
        {
            Item = item;
        }

        public void Add(int amount)
        {
            if (int.MaxValue - Amount < amount)
            {
                SetAmount(int.MaxValue);
                return;
            }

            SetAmount(Amount + amount);
        }

        public void Remove(int amount)
        {
            if (Amount < amount)
            {
                SetAmount(0);
                return;
            }

            SetAmount(Amount - amount);
        }

        public void Clear()
        {
            SetAmount(0);
        }

        private void SetAmount(int value)
        {
            int delta = value - Amount;
            Amount = value;
            Changed?.Invoke(Item, delta);
        }
    }
}