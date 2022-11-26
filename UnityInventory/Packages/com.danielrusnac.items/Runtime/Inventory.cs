using System;
using System.Collections.Generic;

namespace Items
{
    public class Inventory : IInventory
    {
        public event Action<ItemChangedData> Changed;
        
        private ISlotFactory _slotFactory;
        private Dictionary<string, ISlot> _slotByID;

        public IEnumerable<ISlot> Slots => _slotByID.Values;

        public Inventory(ISlotFactory slotFactory)
        {
            _slotFactory = slotFactory;
            _slotByID = new Dictionary<string, ISlot>();
        }
        
        public bool ContainsSlot(IItem item)
        {
            return _slotByID.ContainsKey(item.ID);
        }

        public ISlot GetOrCreateSlot(IItem item)
        {
            if (!ContainsSlot(item))
                CreateSlot(item);

            return _slotByID[item.ID];
        }
        
        private void CreateSlot(IItem item)
        {
            Slot slot = new Slot(item);
            slot.Changed += OnSlotChanged;
            _slotByID.Add(item.ID, slot);
        }

        private void RemoveSlot(IItem item)
        {
            _slotByID.Remove(item.ID);
        }
        
        private void OnSlotChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);

            if (data.NewAmount == 0)
                RemoveSlot(data.Item);
        }
    }
}