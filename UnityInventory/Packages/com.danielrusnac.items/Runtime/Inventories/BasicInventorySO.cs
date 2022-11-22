using Items.Factories;
using UnityEngine;

namespace Items.Inventories
{
    [CreateAssetMenu(menuName = "Items/Inventory", fileName = "inventory_")]
    public class BasicInventorySO : InventorySO<BasicInventory>
    {
        private BasicInventory _inventory;

        protected override BasicInventory Inventory => _inventory;

        protected override void OnEnable()
        {
            _inventory = new BasicInventory(new SlotFactory());
            
            base.OnEnable();
        }
    }
}