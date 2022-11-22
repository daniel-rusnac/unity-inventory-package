using Items.Factories;

namespace Items.Inventories
{
    public class BasicInventoryComponent : InventoryComponent<BasicInventory>
    {
        private BasicInventory _inventory;
        
        private void Awake()
        {
            _inventory = new BasicInventory(new SlotFactory());
        }

        protected override BasicInventory Inventory => _inventory;
    }
}