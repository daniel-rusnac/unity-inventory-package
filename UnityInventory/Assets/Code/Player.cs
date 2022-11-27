using FoggyWoods.Inventories;
using FoggyWoods.Inventories.UI;
using UnityEngine;

namespace InventoriesDebug
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private InventoryDisplay _inventoryDisplay;

        private IInventory _inventory;

        private void Start()
        {
            _inventory = new Inventory(new SlotFactory());
            _inventoryDisplay.Initialize(_inventory);
        }
    }
}
