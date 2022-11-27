using System;

namespace FoggyWoods.Inventories
{
    [Serializable]
    public struct ItemChangedData
    {
        public IItem Item;
        public int OldAmount;
        public int NewAmount;
        public int Delta;

        public ItemChangedData(IItem item, int oldAmount, int newAmount)
        {
            Item = item;
            OldAmount = oldAmount;
            NewAmount = newAmount;
            Delta = newAmount - oldAmount;
        }
    }
}