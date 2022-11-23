﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Inventory", fileName = "inventory_")]
    public class Inventory : ScriptableObject, ISerializationCallbackReceiver
    {
        public event Action<ItemChangedData> Changed;

        // TODO Remove this array after creating a custom editor.
        [SerializeField] private Slot[] _slots;

        private Dictionary<int, Slot> _slotByID = new();

        public IEnumerable<Slot> Slots => _slotByID.Values;
        
        public bool ContainsSlot(Item item)
        {
            return _slotByID.ContainsKey(item.ID);
        }

        public Slot GetSlotOrCreate(Item item)
        {
            if (!ContainsSlot(item))
                CreateSlot(item);

            return _slotByID[item.ID];
        }

        private void CreateSlot(Item item)
        {
            Slot slot = new Slot(item);
            slot.Changed += OnSlotChanged;
            _slotByID.Add(item.ID, slot);
        }

        private void RemoveSlot(Item item)
        {
            _slotByID.Remove(item.ID);
        }

        private void OnSlotChanged(ItemChangedData data)
        {
            Changed?.Invoke(data);

            if (data.NewAmount == 0)
                RemoveSlot(data.Item);
        }

        public void OnBeforeSerialize()
        {
            _slots = _slotByID.Values.ToArray();
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}