using Items;
using Items.Factories;
using Items.Inventories;
using Items.UI;
using UnityEngine;

namespace Code
{
    public class InventoryUser : MonoBehaviour
    {
        [SerializeField] private int _amount = 10;
        [SerializeField] private Item _item;
        [SerializeField] private InventoryUI _inventoryUI;

        private IInventory _inventory;

        private void Awake()
        {
            _inventory = new BasicInventory(new SlotFactory());
        }

        [ContextMenu(nameof(AssignToUI))]
        private void AssignToUI()
        {
            _inventoryUI.SetInventory(_inventory);
        }

        [ContextMenu(nameof(RemoveFromUI))]
        private void RemoveFromUI()
        {
            _inventoryUI.RemoveInventory();
        }

        [ContextMenu(nameof(AddItems))]
        private void AddItems()
        {
            _inventory.GetSlotOrCreate(_item).Amount += _amount;
        }
    }
}