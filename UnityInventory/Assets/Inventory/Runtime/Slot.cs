using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct Slot : IEquatable<Slot>
    {
        private const int MIN_COUNT = 0;

        [SerializeField] private string id;
        [SerializeField] private int count;
        [SerializeField] private int max;

        public string ID => id;
        public int Count => count;
        public int Max => max;

        public Slot(string id, int count, int max)
        {
            this.id = id;
            this.count = count;
            this.max = max;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Slot && Equals((Slot) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (id != null ? id.GetHashCode() : 0);
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
            return ID == other.ID && Count == other.Count;
        }

        public static Slot operator +(Slot packet, int count)
        {
            return new Slot(packet.ID, ClampCount(packet.Count + count), packet.max);
        }

        public static Slot operator *(Slot packet, int multiplier)
        {
            return new Slot(packet.ID, ClampCount(packet.Count * multiplier), packet.max);
        }

        public static Slot operator /(Slot packet, int divisor)
        {
            if (divisor == 0)
            {
                return new Slot(packet.ID, packet.Count, packet.max);
            }

            return new Slot(packet.ID, ClampCount(packet.Count / divisor), packet.max);
        }

        public static Slot operator -(Slot packet, int count)
        {
            return new Slot(packet.ID, ClampCount(packet.Count - count), packet.max);
        }

        private static int ClampCount(int unclampedCount)
        {
            return Mathf.Max(unclampedCount, MIN_COUNT);
        }
    }
}