using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Dynamic Inventory",
        menuName = InventoryConstants.CREATE_SO_MENU + "Dynamic Inventory")]
    public class DynamicInventorySO : InventorySO
    {
        private Dictionary<int, Dictionary<int, Slot>> _slotByID = new Dictionary<int, Dictionary<int, Slot>>();
        private Dictionary<int, Dictionary<int, long>> _limitByID = new Dictionary<int, Dictionary<int, long>>();

        public override void AddAmount(ItemSO item, long amount)
        {
            if (!item.IsInstance)
            {
                item = item.GetInstance();
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
            
            long oldAmount = GetAmount(item);
            
            if (long.MaxValue - amount < _slotByID[item.StaticID][item.DynamicID].Amount)
            {
                _slotByID[item.StaticID][item.DynamicID].Amount = long.MaxValue;
            }
            else
            {
                _slotByID[item.StaticID][item.DynamicID].Amount += amount;
            }
           
            ClampAmount(item);
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

            if (!Contains(item, 0))
                return;

            long oldAmount = GetAmount(item);
            _slotByID[item.StaticID][item.DynamicID].Amount -= amount;
            ClampAmount(item);
            long delta = GetAmount(item) - oldAmount;
            OnChanged(item, delta);

            if (_slotByID[item.StaticID][item.DynamicID].ShouldBeRemoved)
            {
                _slotByID[item.StaticID].Remove(item.DynamicID);
            }
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
                _limitByID.Add(item.StaticID, new Dictionary<int, long>());
            }

            if (!_limitByID.ContainsKey(item.StaticID))
            {
                _limitByID[item.StaticID].Add(item.DynamicID, item.IsDynamic ? 1 : limit);
            }

            long oldAmount = GetAmount(item);
            _limitByID[item.StaticID][item.DynamicID] = item.IsDynamic ? 1 : limit;
            ClampAmount(item);
            long delta = GetAmount(item) - oldAmount;
            OnChanged(item, delta);
        }

        public override long GetAmount(ItemSO item)
        {
            if (!Contains(item, 0))
                return 0;

            return _slotByID[item.StaticID][item.DynamicID].Amount;
        }

        public override bool Contains(ItemSO item, long amount = 1)
        {
            return _slotByID.ContainsKey(item.StaticID) &&
                   _slotByID[item.StaticID].ContainsKey(item.DynamicID) &&
                   _slotByID[item.StaticID][item.DynamicID].Amount >= amount;
        }

        public override long GetLimit(ItemSO item)
        {
            if (Contains(item, 0))
            {
                return _limitByID[item.StaticID][item.DynamicID];
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
            _slotByID[item.StaticID].Add(item.DynamicID, InventoryUtility.CreateSlot(item));
            SetLimit(item, -1);
        }

        private void ClampAmount(ItemSO item)
        {
            if (!Contains(item, 0))
                return;
            
            long amount = _slotByID[item.StaticID][item.DynamicID].Amount;
            long limit = GetLimit(item);

            if (amount < 0)
            {
                amount = 0;
            }

            if (limit != -1 && amount > limit)
            {
                amount = limit;
            }

            _slotByID[item.StaticID][item.DynamicID].Amount = amount;
        }
    }
}