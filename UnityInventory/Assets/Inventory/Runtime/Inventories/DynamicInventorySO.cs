﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Dynamic Inventory",
        menuName = InventoryConstants.CREATE_SO_MENU + "Dynamic Inventory")]
    public class DynamicInventorySO : InventorySO
    {
        private readonly Dictionary<int, Dictionary<int, Slot>> _slotByID = new Dictionary<int, Dictionary<int, Slot>>();
        private readonly Dictionary<int, long> _limitByID = new Dictionary<int, long>();

        public override void AddAmount(ItemSO item, long amount)
        {
            ItemSO originalItem = item;
            long oldAmount = GetAmount(item);
            long iteration = item.IsDynamic ? (ClampAmount(oldAmount + amount, GetLimit(item)) - oldAmount) : 1;
            amount = item.IsDynamic ? 1 : amount;

            for (long i = 0; i < iteration; i++)
            {
                if (!originalItem.IsInstance)
                {
                    item = originalItem.GetInstance();
                }
            
                if (amount < 0)
                {
                    RemoveAmount(item, -amount);
                    return;
                }

                if (!_slotByID.ContainsKey(item.StaticID))
                {
                    _slotByID.Add(item.StaticID, new Dictionary<int, Slot>());
                }

                if (!_slotByID[item.StaticID].ContainsKey(item.DynamicID))
                {
                    CreateSlot(item);
                }

                if (long.MaxValue - amount < _slotByID[item.StaticID][item.DynamicID].Amount)
                {
                    _slotByID[item.StaticID][item.DynamicID].Amount = long.MaxValue;
                }
                else
                {
                    _slotByID[item.StaticID][item.DynamicID].Amount = ClampAmount(oldAmount + amount, item.IsDynamic ? 1 : GetLimit(item));
                }
            }
            
            long delta = GetAmount(item) - oldAmount;
            OnChanged(item, delta);
        }

        public override void RemoveAmount(ItemSO item, long amount)
        {
            if (amount < 0)
            {
                AddAmount(item, -amount);
                return;
            }

            if (!_slotByID.ContainsKey(item.StaticID) || !item.IsDynamic && !_slotByID[item.StaticID].ContainsKey(item.DynamicID))
                return;

            long oldAmount = GetAmount(item);

            if (item.IsDynamic)
            {
                if (_slotByID[item.StaticID].Count > 0)
                {
                    long i = 0;

                    while (i < amount)
                    {
                        _slotByID[item.StaticID].Remove(_slotByID[item.StaticID].Keys.First());
                        i++;
                        
                        if (_slotByID[item.StaticID].Count == 0)
                            break;
                    }
                }
            }
            else
            {
                _slotByID[item.StaticID][item.DynamicID].Amount = ClampAmount(oldAmount - amount, GetLimit(item));
            }

            long newAmount = GetAmount(item); 
            long delta = newAmount- oldAmount;

            if (newAmount == 0 && GetLimit(item) == -1)
            {
                _slotByID[item.StaticID].Remove(item.DynamicID);
            }
            
            OnChanged(item, delta);
        }

        public override void SetAmount(ItemSO item, long amount)
        {
            if (!Contains(item, 0))
            {
                AddAmount(item, amount);
                return;
            }

            long amountOffset = amount - GetAmount(item);

            if (amountOffset > 0)
            {
                AddAmount(item, amountOffset);
            }
            else
            {
                RemoveAmount(item, -amountOffset);
            }
        }

        public override void SetLimit(ItemSO item, long limit)
        {
            if (limit < -1)
            {
                limit = -1;
            }
            
            if (!_limitByID.ContainsKey(item.StaticID))
            {
                _limitByID.Add(item.StaticID, limit);
            }
            else
            {
                _limitByID[item.StaticID] = limit;
            }
            
            long oldAmount = GetAmount(item);
            long clampedAmount = ClampAmount(oldAmount, limit);
            
            if (clampedAmount == oldAmount)
                return;
            
            SetAmount(item, clampedAmount);
        }

        public override long GetAmount(ItemSO item)
        {
            if (!_slotByID.ContainsKey(item.StaticID))
                return 0;
            
            long amount = 0;
            
            foreach (Slot slot in _slotByID[item.StaticID].Values)
            {
                amount += slot.Amount;
            }

            return amount;
        }

        public override bool Contains(ItemSO item, long amount = 1)
        {
            return GetAmount(item) >= amount;
        }

        public override long GetLimit(ItemSO item)
        {
            if (_limitByID.ContainsKey(item.StaticID))
            {
                return _limitByID[item.StaticID];
            }
            
            return -1;
        }

        public override string Serialize()
        {
            return "";
        }

        public override void Deserialize(string data)
        {
        }

        public override ItemSO[] GetInstances()
        {
            List<ItemSO> instances = new List<ItemSO>();

            foreach (Dictionary<int, Slot> slotByDynamicID in _slotByID.Values)
            {
                foreach (int dynamicID in slotByDynamicID.Keys)
                {
                    if (slotByDynamicID[dynamicID].Amount == 0)
                        continue;

                    if (InventoryUtility.TryGetItem(dynamicID, out var item))
                    {
                        instances.Add(item);
                        continue;
                    }

                    Debug.LogWarning($"Failed to load item with dynamic id: {dynamicID}");
                }
            }

            return instances.ToArray();
        }

        public override T[] GetInstances<T>()
        {
            List<T> instances = new List<T>();

            foreach (Dictionary<int, Slot> slotByDynamicID in _slotByID.Values)
            {
                foreach (int dynamicID in slotByDynamicID.Keys)
                {
                    if (slotByDynamicID[dynamicID].Amount == 0)
                        continue;

                    if (InventoryUtility.TryGetItem(dynamicID, out var item))
                    {
                        if (item as T != null)
                        {
                            instances.Add((T) item);
                        }

                        continue;
                    }

                    Debug.LogWarning($"Failed to load item with DynamicID: {dynamicID}");
                }
            }

            return instances.ToArray();
        }

        private void CreateSlot(ItemSO item)
        {
            _slotByID[item.StaticID].Add(item.DynamicID,  new Slot(item.StaticID, item.DynamicID));
        }

        private long ClampAmount(long unclampedAmount, long limit)
        {
            if (unclampedAmount < 0)
            {
                return 0;
            }

            if (limit != -1 && unclampedAmount > limit)
            {
                return limit;
            }

            return unclampedAmount;
        }
    }
}