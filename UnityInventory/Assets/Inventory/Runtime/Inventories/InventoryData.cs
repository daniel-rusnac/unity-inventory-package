using System;

namespace InventorySystem
{
    [Serializable]
    public class InventoryData
    {
        public int[] StaticIDs;
        public long[] Limits;
        
        public int[][] DynamicIDs;
        public long[] Amounts;
        public object[] DynamicItemsData;

        public bool IsEmpty => StaticIDs == null || StaticIDs.Length == 0;
    }
}