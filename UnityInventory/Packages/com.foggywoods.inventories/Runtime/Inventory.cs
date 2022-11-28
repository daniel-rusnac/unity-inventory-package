using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoggyWoods.Inventories
{
    public class Inventory : IInventory
    {
        public event Action<ItemChangedData> Changed;
        public event Action<ISlot> SlotAdded;
        public event Action<ISlot> SlotRemoved;

        private readonly ISlotFactory _slotFactory;
        private readonly Dictionary<string, ISlot> _slotByID;

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

        public InventoryData Serialize()
        {
            return new InventoryData()
            {
                IDs = _slotByID.Keys.ToArray(),
                Amounts = _slotByID.Values.Select(slot => slot.Amount).ToArray(),
                Limits = _slotByID.Values.Select(slot => slot.Limit).ToArray()
            };
        }

        public void Deserialize(InventoryData data, IItemDatabase database)
        {
            if (!data.IsValid)
                return;

            for (int i = 0; i < data.IDs.Length; i++)
            {
                if (!database.TryLoadItem(data.IDs[i], out IItem item))
                {
                    Debug.LogWarning($"Couldn't deserialize item with id {data.IDs[i]}!");
                    continue;
                }

                ISlot slot = GetOrCreateSlot(item);
                slot.Amount = data.Amounts[i];
                slot.Limit = data.Limits[i];
            }
        }

        private void CreateSlot(IItem item)
        {
            ISlot slot = _slotFactory.CreateSlot(item);
            slot.Changed += OnSlotChanged;
            _slotByID.Add(item.ID, slot);
            SlotAdded?.Invoke(slot);
        }

        private void RemoveSlot(IItem item)
        {
            if (!ContainsSlot(item))
                return;

            ISlot slot = _slotByID[item.ID];
            _slotByID.Remove(item.ID);
            SlotRemoved?.Invoke(slot);
        }

        private void OnSlotChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);

            if (data.NewAmount == 0)
                RemoveSlot(data.Item);
        }
    }
}