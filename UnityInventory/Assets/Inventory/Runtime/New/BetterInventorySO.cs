using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.New
{
    [Serializable]
    public class Slot
    {
        [SerializeField] private ItemSO _item;
        [SerializeField] private long _amount;
        [SerializeField] private long _limit;

        public ItemSO Item => _item;
        public long Amount => _amount;
        public long Limit => _limit;

        public Slot(ItemSO item)
        {
            _item = item;
        }

        public void Add(long amount)
        {
            _amount += amount;
        }

        public void Remove(long amount)
        {
            _amount -= amount;
        }
    }
    
    [CreateAssetMenu(fileName = "Inventory", menuName = InventoryConstants.CREATE_SO_MENU + "Inventory")]
    public class BetterInventorySO : ScriptableObject
    {
        [SerializeField] private List<Slot> _debugSlots;

        private Dictionary<int, Slot> _slotByID = new Dictionary<int, Slot>();

        public void Add(ItemSO item, long amount)
        {
            if (amount < 0)
            {
                Remove(item, -amount);
            }
            
            if (!_slotByID.ContainsKey(item.DynamicID))
            {
                _slotByID.Add(item.DynamicID, new Slot(item));    
            }
            
            _slotByID[item.DynamicID].Add(amount);
            
            RefreshDebugSlots();
        }

        public void Remove(ItemSO item, long amount)
        {
            if (amount < 0)
            {
                Add(item, -amount);
            }
            
            if (!_slotByID.ContainsKey(item.DynamicID))
                return;
            
            _slotByID[item.DynamicID].Remove(amount);
            
            RefreshDebugSlots();
        }

        public void SetAmount(ItemSO item, long amount)
        {
            if (!_slotByID.ContainsKey(item.DynamicID))
            {
                Add(item, amount);
                return;
            }

            long amountOffset = amount - Get(item);

            if (amountOffset > 0)
            {
                Add(item, amountOffset);
            }
            else
            {
                Remove(item, -amountOffset);
            }
        }

        public long Get(ItemSO item)
        {
            if (!_slotByID.ContainsKey(item.DynamicID))
            {
                return 0;
            }

            return _slotByID[item.DynamicID].Amount;
        }

        public long GetLimit(ItemSO item)
        {
            return -1;
        }

        public void SetLimit(ItemSO item, long amount)
        {
            
        }
        
        private void RefreshDebugSlots()
        {
            _debugSlots = _slotByID.Values.Select(slot => slot).ToList();
        }
    }
}