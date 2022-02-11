using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class InventorySO : ScriptableObject
    {
        public event Action Changed;
        public event Action<ItemSO, int> ChangedAmount;

        private readonly Dictionary<ItemSO, Slot> _slotByID = new Dictionary<ItemSO, Slot>();
        
        public void Add(ItemSO item, int amount)
        {
            if (amount == 0)
                return;
            
            if (amount < 0)
                Remove(item, amount);

            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            _slotByID[item] += amount;
        }
        
        public void Remove(ItemSO item, int amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                Add(item, -amount);
                return;
            }
            
            if (!_slotByID.ContainsKey(item))
                return;

            _slotByID[item] -= amount;
        }

        public bool Contains(ItemSO item, int amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(item) && _slotByID[item].Count >= amount;
        }

        public int GetCount(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Count;
            }

            return 0;
        }

        /// <summary>
        /// Set the maximum value for an item.
        /// </summary>
        /// <param name="item">Item ID to set maximum valur for.</param>
        /// <param name="max">The maximum value. -1 is unlimited.</param>
        public void SetMax(ItemSO item, int max)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0, max));
            }
            else
            {
                _slotByID[item] = _slotByID[item].SetMax(max);
            }
        }

        public int GetMax(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Count;
            }

            return InventoryUtility.DEFAULT_MAX;
        }

        /// <returns>How many slots with count > 0 are in the inventory.</returns>
        public int GetActiveSlotsCount()
        {
            return _slotByID.Count(pair => pair.Value.Count > 0);
        }

        public ItemSO[] GetItems()
        {
            return _slotByID.Where(pair => pair.Value.Count > 0).Select(pair => pair.Key).ToArray();
        }

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear()
        {
            foreach (ItemSO item in _slotByID.Keys)
            {
                OnChanged(item, _slotByID[item].Count);
            }
            
            _slotByID.Clear();
            OnChanged();
        }
        
        public override string ToString()
        {
            if (_slotByID.Count == 0)
                return "[Empty]";

            string result = "[";

            result += string.Join("] [", _slotByID.Values.Select(slot => slot.ToString()).ToArray());

            return result + "]";
        }
        
        private void OnChanged(ItemSO item, int delta)
        {
            ChangedAmount?.Invoke(item, delta);
            OnChanged();
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }

        [Obsolete("Use the inventory directly.")]
        public InventorySO GetInventory => this;

        [Obsolete("Use GetItems instead.")]
        public ItemSO[] GetAllItems()
        {
            return GetItems();
        }

        [Obsolete("Use Inventory.Changed instead.")]
        public void Register(Action action)
        {
            Changed += action;
        }

        [Obsolete("Use Inventory.Changed instead.")]
        public void Unregister(Action action)
        {
            Changed -= action;
        }
    }
}