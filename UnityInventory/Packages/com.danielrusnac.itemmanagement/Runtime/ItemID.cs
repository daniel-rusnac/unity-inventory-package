using System;

namespace Items
{
    [Serializable]
    public struct ItemID : IEquatable<ItemID>
    {
        public int ItemInstanceID;
        public int DefinitionID;

        public ItemID(int definitionID, int itemInstanceID)
        {
            DefinitionID = definitionID;
            ItemInstanceID = itemInstanceID;
        }

        public bool Equals(ItemID other)
        {
            return ItemInstanceID == other.ItemInstanceID && DefinitionID == other.DefinitionID;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemInstanceID, DefinitionID);
        }
    }
}