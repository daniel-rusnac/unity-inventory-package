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
            OnChanged(item, amount);
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
            OnChanged(item, amount);
        }

        public bool Contains(ItemSO item, int amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(item) && _slotByID[item].Amount >= amount;
        }

        public int GetAmount(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Amount;
            }

            return 0;
        }

        public void SetAmount(ItemSO item, int amount)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            int delta = amount - _slotByID[item].Amount;
            _slotByID[item] += delta;
            
            OnChanged(item, delta);
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
                int lastValue = _slotByID[item].Amount; 
                _slotByID[item] = _slotByID[item].SetMax(max);

                OnChanged(item, lastValue - _slotByID[item].Amount);
            }
        }

        public int GetMax(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Amount;
            }

            return InventoryUtility.DEFAULT_MAX;
        }

        /// <returns>How many slots with count > 0 are in the inventory.</returns>
        public int GetActiveSlotsCount()
        {
            return _slotByID.Count(pair => pair.Value.Amount > 0);
        }

        public ItemSO[] GetItems()
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key).ToArray();
        }

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear()
        {
            foreach (ItemSO item in _slotByID.Keys)
            {
                OnChanged(item, _slotByID[item].Amount);
            }
            
            _slotByID.Clear();
        }
        
        public override string ToString()
        {
            if (_slotByID.Count == 0)
                return "[Empty]";

            string result = "[";

            result += string.Join("] [", _slotByID.Values.Select(slot => slot.ToString()).ToArray());

            return result + "]";
        }

        public string Serialize()
        {
            string[] values = 
            {
                string.Join(",", _slotByID.Keys.Select(so => so.ID)),
                string.Join(",", _slotByID.Values.Select(slot => slot.Amount)),
                string.Join(",", _slotByID.Values.Select(slot => slot.Max))
            };
            
            return string.Join("|", values);
        }

        public void Deserialize(string content)
        {
            string[] values = content.Split('|');

            int count = values.Length;

            if (count != 3)
            {
                Debug.LogWarning($"Couldn't deserialize content: {content}!", this);
                return;
            }

            int[] ids = values[0].Split(',').Select(int.Parse).ToArray();
            int[] amount = values[1].Split(',').Select(int.Parse).ToArray();
            int[] max = values[2].Split(',').Select(int.Parse).ToArray();

            for (int i = 0; i < count; i++)
            {
                if (InventoryUtility.TryGetItem(ids[i], out ItemSO item))
                {
                    if (!_slotByID.ContainsKey(item))
                    {
                        Add(item, amount[i]);
                        SetMax(item, max[i]);
                    }
                    else
                    {
                        SetMax(item, max[i]);
                        SetAmount(item, amount[i]);
                    }
                }
                else
                {
                    Debug.LogWarning($"Couldn't deserialize id: {ids[i]}! Maybe you changed it after saving?", this);
                }
            }
        }
        
        private void OnChanged(ItemSO item, int delta)
        {
            if (delta == 0)
                return;
            
            ChangedAmount?.Invoke(item, delta);
            OnChanged();
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }
    }
}