using System;

namespace InventorySystem
{
    [Serializable]
    public class InventoryData
    {
        public int[] StaticIDs;
        public int[] DynamicIDs;
        public long[] Limits;
        public long[] Amounts;
    }
}