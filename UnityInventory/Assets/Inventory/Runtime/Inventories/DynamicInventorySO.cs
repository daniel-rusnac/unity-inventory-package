using System.Collections.Generic;
using InventorySystem.Slots;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Dynamic Inventory", menuName = InventoryConstants.CREATE_SO_MENU + "Dynamic Inventory")]
    public class DynamicInventorySO : InventorySO
    {
        private Dictionary<int, Dictionary<int, Slot>> _slotByID = new Dictionary<int, Dictionary<int, Slot>>();

        public override void AddAmount(ItemSO item, long amount)
        {
            if (amount < 0)
            {
                RemoveAmount(item, -amount);
            }

            if (!_slotByID.ContainsKey(item.StaticID))
            {
                _slotByID.Add(item.DynamicID, new Dictionary<int, Slot>());
            }

            if (!_slotByID[item.StaticID].ContainsKey(item.DynamicID))
            {
                _slotByID[item.StaticID].Add(item.DynamicID, InventoryUtility.CreateSlot(item));
            }

            _slotByID[item.StaticID][item.DynamicID].Add(amount);
        }

        public override void RemoveAmount(ItemSO item, long amount)
        {
            if (amount < 0)
            {
                AddAmount(item, -amount);
            }

            if (!Contains(item, 0))
                return;

            _slotByID[item.StaticID][item.DynamicID].Remove(amount);
        }

        public override long GetAmount(ItemSO item)
        {
            if (!Contains(item, 0))
                return 0;

            return _slotByID[item.StaticID][item.DynamicID].Amount;
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

        public override bool Contains(ItemSO item, long amount = 1)
        {
            return _slotByID.ContainsKey(item.StaticID) && 
                   _slotByID[item.StaticID].ContainsKey(item.DynamicID) &&
                   _slotByID[item.StaticID][item.DynamicID].Amount >= amount;
        }

        public override long GetLimit(ItemSO item)
        {
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

            foreach (Dictionary<int,Slot> slotByDynamicID in _slotByID.Values)
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

            foreach (Dictionary<int,Slot> slotByDynamicID in _slotByID.Values)
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

        public override void SetLimit(ItemSO item, long amount)
        {
            if (!Contains(item, 0))
            {
                AddAmount(item, 0);
            }
            
            _slotByID[item.StaticID][item.DynamicID].SetLimit(amount);
        }
    }
}