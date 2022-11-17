using ItemManagement;
using UnityEngine;

namespace Code
{
    public class InventoryUser : MonoBehaviour
    {
        [SerializeField] private int _amount = 1;
        [SerializeField] private ItemDefinition _item;
        [SerializeField] private InventoryComponent _inventory;

        [ContextMenu(nameof(AddItem))]
        private void AddItem()
        {
            if (!_inventory.ContainsSlot(_item.ID))
                _inventory.AddSlot(_item.ID, new Slot(new Item(_item)));
            
            _inventory.GetSlot(_item.ID).Amount += _amount;
        }
    }
}