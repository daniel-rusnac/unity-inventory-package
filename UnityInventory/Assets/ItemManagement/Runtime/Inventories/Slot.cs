using System;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    public class Slot : ISlot
    {
        public event Action<IItem, int> Changed;
        
        private int _amount;

        public int Amount
        {
            get => _amount;
            set => SetAmount(value);
        }

        public IItem Item { get; }

        public Slot(IItem item)
        {
            Item = item;
        }

        private void SetAmount(int value)
        {
            int delta = value - _amount;
            _amount = value;
            Changed?.Invoke(Item, delta);
        }
    }
}