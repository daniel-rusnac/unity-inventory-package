using System;
using UnityEngine;

namespace ItemManagement
{
    public class Slot : ISlot
    {
        public event Action<ItemChangedData> Changed;

        private int _amount;
        
        public int MaxAmount { get; set; }
        public IItemDefinition ItemDefinition { get; }
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

        public Slot(IItem item)
        {
            Item = item;
            ItemDefinition = item.Definition;
            MaxAmount = -1;
        }
    }
}