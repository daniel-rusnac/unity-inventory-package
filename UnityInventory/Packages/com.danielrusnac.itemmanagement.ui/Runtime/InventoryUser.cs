using UnityEngine;

namespace ItemManagement.UI
{
    public class InventoryUser : MonoBehaviour
    {
        [SerializeField] private InventoryUI _inventoryUI;

        private IInventory _inventory;

        private void Initialize()
        {
            _inventory = new Inventory(new SlotFactory());
            _inventoryUI.SetInventory(_inventory);
        }
    }
}