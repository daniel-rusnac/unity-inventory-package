using UnityEngine;

namespace InventorySystem.Slots
{
    public class DynamicSlot : Slot
    {
        private int _staticID;
        private int _dynamicID;
        private long _amount;
        private long _limit;

        public override int StaticID => _staticID;
        public override int DynamicID => _dynamicID;
        public override long Amount => _amount;
        public override long Limit => _limit;

        private bool IsDynamicItem => _staticID != _dynamicID;

        public DynamicSlot(int staticID, int dynamicID)
        {
            _staticID = staticID;
            _dynamicID = dynamicID;

            _limit = IsDynamicItem ? 1 : -1;
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

        public override void SetLimit(long limit)
        {
            if (limit < InventoryUtility.DEFAULT_LIMIT)
                return;

            if (IsDynamicItem)
            {
                Debug.Log($"Trying to set the limit [{limit}] for a dynamic item! Setting back to [1].");
                limit = 1;
            }

            _limit = limit;
            _amount = ClampAmount(_amount);
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