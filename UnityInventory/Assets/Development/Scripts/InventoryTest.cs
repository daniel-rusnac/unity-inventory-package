using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    public class InventoryTest : MonoBehaviour
    {
        [SerializeField] private WeaponSO _weaponShop;
        [SerializeField] private WeaponSO _myWeapon;
        [SerializeField] private int _equippedID;
        
        [SerializeField] private InventorySO _inventory;

        [ContextMenu(nameof(Buy))]
        private void Buy()
        {
            _inventory.AddAmount(_weaponShop, 1);
        }

        [ContextMenu(nameof(Equip))]
        private void Equip()
        {
            _myWeapon = _inventory.GetInstance(_weaponShop, _equippedID);

            if (_myWeapon != null)
            {
                _equippedID = _myWeapon.DynamicID;
            }
        }

        [ContextMenu(nameof(Unequip))]
        private void Unequip()
        {
            _myWeapon = null;
        }
    }
}