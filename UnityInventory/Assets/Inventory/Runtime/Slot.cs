using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct Slot : IEquatable<Slot>
    {
        [SerializeField] private readonly byte id;
        [SerializeField] private int count;
        [SerializeField] private int max;
        
        public int Count => count;
        public int Max => max;
        
        public Slot(byte id, int count, int max = InventoryUtility.DEFAULT_MAX)
        {
            this.id = id;
            this.count = count;
            this.max = max;
        }

        public void SetMax(int value)
        {
            if (value < InventoryUtility.DEFAULT_MAX)
                return;
            
            max = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Slot && Equals((Slot)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = id.GetHashCode();
                hashCode = (hashCode * 397) ^ count;
                hashCode = (hashCode * 397) ^ max;
                return hashCode;
            }
        }

        public static bool operator ==(Slot a, Slot b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Slot a, Slot b)
        {
            return !(a == b);
        }

        public bool Equals(Slot other)
        {
            return id == other.id && Count == other.Count;
        }

        public static Slot operator +(Slot packet, int count)
        {
            return new Slot(packet.id, ClampCount(packet.Count + count), packet.max);
        }

        public static Slot operator *(Slot packet, int multiplier)
        {
            return new Slot(packet.id, ClampCount(packet.Count * multiplier), packet.max);
        }

        public static Slot operator /(Slot packet, int divisor)
        {
            if (divisor == 0)
            {
                return new Slot(packet.id, packet.Count, packet.max);
            }

            return new Slot(packet.id, ClampCount(packet.Count / divisor), packet.max);
        }

        public static Slot operator -(Slot packet, int count)
        {
            return new Slot(packet.id, ClampCount(packet.Count - count), packet.max);
        }

        private static int ClampCount(int unclampedCount)
        {
            return Mathf.Max(unclampedCount, 0);
        }
    }
}