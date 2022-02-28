using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    public class InventoryTest : MonoBehaviour
    {
        [SerializeField] private string _saveKey = "sk";
        [SerializeField] private WeaponSO _weaponShop;
        [SerializeField] private WeaponSO _myWeapon;
        [SerializeField] private int _equippedID;
        [SerializeField] private InventorySO _inventory;

        [ContextMenu(nameof(Buy))]
        private void Buy()
        {
            _inventory.Add(_weaponShop);
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

        [ContextMenu(nameof(Sell))]
        private void Sell()
        {
            _inventory.Remove(_myWeapon);
        }

        [ContextMenu(nameof(Save))]
        private void Save()
        {
            ES3.Save(_saveKey, _inventory.Serialize());
        }

        [ContextMenu(nameof(Load))]
        private void Load()
        {
            _inventory.Deserialize(ES3.Load(_saveKey, _inventory.Serialize()));
        }
    }
}