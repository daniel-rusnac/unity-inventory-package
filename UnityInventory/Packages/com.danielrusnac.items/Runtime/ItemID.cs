using System;

namespace Items
{
    [Serializable]
    public struct ItemID : IEquatable<ItemID>
    {
        public int ID;

        public ItemID(int id)
        {
            ID = id;
        }

        public bool Equals(ItemID other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}