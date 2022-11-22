using System;

namespace Items
{
    [Serializable]
    public struct ItemChangedData
    {
        public Item Item;
        public int OldAmount;
        public int NewAmount;
        public int Delta;

        public ItemChangedData(Item item, int oldAmount, int newAmount)
        {
            Item = item;
            OldAmount = oldAmount;
            NewAmount = newAmount;
            Delta = newAmount - oldAmount;
        }
    }
}