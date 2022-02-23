using System;
using InventorySystem.New;
using UnityEngine;

namespace InventorySystem.Slots
{
    [Serializable]
    public class DynamicSlot : Slot
    {
        [SerializeField] private int _staticID;
        [SerializeField] private int _dynamicID;
        [SerializeField] private long _amount;
        [SerializeField] private long _limit;

        public override int StaticID => _staticID;
        public override int DynamicID => _dynamicID;
        public override long Amount => _amount;
        public override long Limit => _limit;

        public DynamicSlot(int staticID, int dynamicID)
        {
            _staticID = staticID;
            _dynamicID = dynamicID;
            _limit = -1;
        }

        public override void Add(long amount)
        {
            if (amount == 0)
                return;
            
            if (amount < 0)
            {
                Remove(-amount);
                return;
            }

            if (long.MaxValue - amount < _amount)
            {
                _amount = long.MaxValue;
                return;
            }
            
            _amount = ClampAmount(_amount + amount);
        }

        public override void Remove(long amount)
        {
            if (amount == 0)
                return;
            
            if (amount < 0)
            {
                Add(-amount);
                return;
            }
            
            _amount = ClampAmount(_amount - amount);
        }
        
        private long ClampAmount(long unclampedCount)
        {
            if (unclampedCount < 0)
                return 0;

            if (_limit != -1 && unclampedCount > _limit)
                return _limit;

            return unclampedCount;
        }
    }
}