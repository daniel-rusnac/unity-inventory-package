using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Static Item", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Static Item")]
    public class StaticItemSO : ItemSO
    {
        public override int DynamicID => StaticID;

        protected override ItemSO OnGetInstance()
        {
            return this;
        }
    }
}