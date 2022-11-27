using FoggyWoods.Inventories;
using FoggyWoods.Inventories.Items;
using FoggyWoods.Inventories.UI;
using UnityEngine;

namespace InventoriesDebug
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _amount;
        [SerializeField] private ItemDefinition _item;
        [SerializeField] private InventoryDisplay _inventoryDisplay;

        private IInventory _inventory;

        private void OnValidate()
        {
            if (Application.isPlaying && _inventory != null)
                UpdateItemAmount();
        }

        private void Start()
        {
            _inventory = new Inventory(new SlotFactory());
            _inventoryDisplay.Initialize(_inventory);
        }

        private void UpdateItemAmount()
        {
            if  (_item == null)
                return;

            _inventory.GetOrCreateSlot(_item).Amount = _amount;
        }
    }
}
