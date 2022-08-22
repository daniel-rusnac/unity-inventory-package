using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ItemManagement.Database;
using ItemManagement.Factories;
using ItemManagement.Items;

namespace ItemManagement.Inventories
{
    [Serializable]
    public class Inventory : IInventory
    {
        private const string IDS = "ids";
        private const string AMOUNTS = "amounts";
        
        public event Action<IItem, int> Changed;

        private readonly Dictionary<string, ISlot> _slotByID;
        private readonly ISlotFactory _slotFactory;
        private readonly IItemDatabase _database;

        public Inventory(ISlotFactory slotFactory, IItemDatabase database)
        {
            _database = database;
            _slotFactory = slotFactory;
            _slotByID = new Dictionary<string, ISlot>();
        }

        public Inventory(SerializationInfo info, StreamingContext context)
        {
            string[] ids = (string[])info.GetValue(IDS, typeof(string[]));
            int[] amounts = (int[])info.GetValue(AMOUNTS, typeof(int[]));
            
            for (int i = 0; i < ids.Length; i++)
            {
                // _database.GetItem(ids[i])
            }
        }
        
        public IItem[] GetItems()
        {
            return _slotByID.Values.Select(slot => slot.Item).ToArray();
        }

        public ISlot[] GetSlots()
        {
            return _slotByID.Values.ToArray();
        }

        public int GetAmount(IItem item)
        {
            return _slotByID.ContainsKey(item.Id) 
                ? _slotByID[item.Id].Amount 
                : 0;
        }

        public void Add(IItem item, int amount)
        {
            GetOrCreateSlot(item).Add(amount);
        }

        public void Remove(IItem item, int amount)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Remove(amount);
        }

        public void Clear()
        {
            foreach (ISlot slot in _slotByID.Values)
                slot.Clear();

            _slotByID.Clear();
        }

        public ISlot GetOrCreateSlot(IItem item)
        {
            if (_slotByID.ContainsKey(item.Id))
                return _slotByID[item.Id];

            var slot = _slotFactory.Create(item);
            _slotByID.Add(item.Id, slot);
            slot.Changed += OnSlotChanged;
            return slot;
        }

        public void RemoveSlot(IItem item)
        {
            if (!_slotByID.ContainsKey(item.Id))
                return;

            _slotByID[item.Id].Changed -= OnSlotChanged;
            _slotByID.Remove(item.Id);
        }

        private void OnSlotChanged(IItem item, int delta)
        {
            Changed?.Invoke(item, delta);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            string[] ids = _slotByID.Keys.ToArray();
            int[] amounts = _slotByID.Values.Select(slot => slot.Amount).ToArray();
            
            info.AddValue(IDS, ids, typeof(string[]));
            info.AddValue(AMOUNTS, amounts, typeof(int[]));
        }
    }
}