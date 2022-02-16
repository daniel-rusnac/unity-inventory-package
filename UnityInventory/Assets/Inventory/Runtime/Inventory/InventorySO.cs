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
        public event Action<ItemSO, long> ChangedAmount;

        private readonly Dictionary<ItemSO, Slot> _slotByID = new Dictionary<ItemSO, Slot>();
        
        public void Add(ItemSO item, long amount)
        {
            if (amount == 0)
                return;
            
            if (amount < 0)
                Remove(item, amount);

            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            _slotByID[item].Add(amount);
            OnChanged(item, amount);
        }
        
        public void Remove(ItemSO item, long amount)
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

            _slotByID[item].Remove(amount);
            OnChanged(item, amount);
        }

        public bool Contains(ItemSO item, long amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(item) && _slotByID[item].Amount >= amount;
        }

        public long GetAmount(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Amount;
            }

            return 0;
        }

        public void SetAmount(ItemSO item, long amount)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            long delta = amount - _slotByID[item].Amount;
            _slotByID[item].Add(delta);
            OnChanged(item, delta);
        }
        
        [Obsolete("Use SetLimit() instead.")]
        public void SetMax(ItemSO item, long max)
        {
            SetLimit(item, max);
        }

        [Obsolete("Use GetLimit() instead.")]
        public long GetMax(ItemSO item)
        {
            return GetLimit(item);
        }
        
        public void SetLimit(ItemSO item, long max)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0, max));
            }
            else
            {
                long lastValue = _slotByID[item].Amount; 
                _slotByID[item].SetLimit(max);

                OnChanged(item, lastValue - _slotByID[item].Amount);
            }
        }

        public void RemoveLimit(ItemSO item)
        {
            SetLimit(item, -1);
        }

        public long GetLimit(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Limit;
            }

            return InventoryUtility.DEFAULT_LIMIT;
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
        /// Return all items of type T
        /// </summary>
        public T[] GetItems<T>() where T : ItemSO
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key as T).Where(item => item != null).ToArray();
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
                string.Join(",", _slotByID.Values.Select(slot => slot.Limit))
            };
            
            return string.Join("|", values);
        }

        public void Deserialize(string content)
        {
            string[] values = content.Split('|');
            
            if (values.Length != 3)
            {
                Debug.LogWarning($"Couldn't deserialize content: {content}!", this);
                return;
            }

            int[] ids = values[0].Split(',').Select(int.Parse).ToArray();
            long[] amount = values[1].Split(',').Select(long.Parse).ToArray();
            int[] max = values[2].Split(',').Select(int.Parse).ToArray();

            int slotCount = ids.Length;

            for (int i = 0; i < slotCount; i++)
            {
                if (InventoryUtility.TryGetItem(ids[i], out ItemSO item))
                {
                    if (!_slotByID.ContainsKey(item))
                    {
                        Add(item, amount[i]);
                        SetLimit(item, max[i]);
                    }
                    else
                    {
                        SetLimit(item, max[i]);
                        SetAmount(item, amount[i]);
                    }
                }
                else
                {
                    Debug.LogWarning($"Couldn't deserialize id: {ids[i]}! Maybe you changed it after saving?", this);
                }
            }
        }

        private void OnChanged(ItemSO item, long delta)
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