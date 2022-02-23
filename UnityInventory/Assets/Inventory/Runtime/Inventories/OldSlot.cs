using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class OldSlot
    {
        [SerializeField] private long _amount;
        [SerializeField] private long _limit;
        [SerializeField] private int _id;

        public long Amount => _amount;
        public long Limit => _limit;

        public OldSlot(int id, long count, long limit = InventoryUtility.DEFAULT_LIMIT)
        {
            _id = id;
            _amount = ClampAmount(count);
            _limit = limit;
        }

        public void SetLimit(long value)
        {
            if (value < InventoryUtility.DEFAULT_LIMIT)
                return;

            _limit = value;
            _amount = ClampAmount(_amount);
        }

        public void Add(long value)
        {
            if (value == 0)
                return;
            
            if (value < 0)
            {
                Remove(-value);
                return;
            }

            if (long.MaxValue - value < _amount)
            {
                _amount = long.MaxValue;
                return;
            }
            
            _amount = ClampAmount(_amount + value);
        }

        public void Remove(long value)
        {
            if (value == 0)
                return;
            
            if (value < 0)
            {
                Add(-value);
                return;
            }
            
            _amount = ClampAmount(_amount - value);
        }
        
        private long ClampAmount(long unclampedCount)
        {
            if (unclampedCount < 0)
                return 0;

            if (_limit != -1 && unclampedCount > _limit)
                return _limit;

            return unclampedCount;
        }

        public override string ToString()
        {
            InventoryUtility.TryGetItem(_id, out ItemSO item);
            return $"{(item == null ? "???" : item.Name)}: {Amount}{(_limit > 0 ? $"/{_limit}" : "")}";
        }
    }
}