using System;

namespace FoggyWoods.Inventories
{
    [Serializable]
    public struct InventoryData
    {
        public string[] IDs;
        public int[] Amounts;
        public int[] Limits;

        public bool IsValid => IDs != null && IDs.Length > 0;
    }
}