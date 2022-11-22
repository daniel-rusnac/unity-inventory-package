using UnityEngine;

namespace Items.UI
{
    public class InventoryUI : MonoBehaviour
    {
        private IInventory _inventory;

        public void SetInventory(IInventory inventory)
        {
            _inventory = inventory;
        }

        public void RemoveInventory()
        {
            _inventory = null;
        }
    }
}