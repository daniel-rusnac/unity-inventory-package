using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct Slot
    {
        [SerializeField] private int _amount;
        [SerializeField] private int _max;
        [SerializeField] private int _id;

        public int Amount => _amount;
        public int Max => _max;

        public Slot(int id, int count, int max = InventoryUtility.DEFAULT_MAX)
        {
            _id = id;
            _amount = ClampCount(count, max);
            _max = max;
        }

        public Slot SetMax(int value)
        {
            if (value < InventoryUtility.DEFAULT_MAX)
                return this;

            return new Slot(_id, _amount, value);
        }

        public static Slot operator +(Slot packet, int count)
        {
            return new Slot(packet._id, ClampCount(packet.Amount + count, packet._max), packet._max);
        }

        public static Slot operator -(Slot packet, int count)
        {
            return new Slot(packet._id, ClampCount(packet.Amount - count, packet._max), packet._max);
        }

        private static int ClampCount(int unclampedCount, int max)
        {
            int value = Mathf.Max(unclampedCount, 0);

            if (max != InventoryUtility.DEFAULT_MAX)
            {
                value = Mathf.Min(value, max);
            }

            return value;
        }

        public override string ToString()
        {
            InventoryUtility.TryGetItem(_id, out ItemSO item);
            return $"{(item == null ? "???" : item.ItemName)}: {Amount}{(_max > 0 ? $"/{_max}" : "")}";
        }
    }
}