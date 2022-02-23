using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Weapon")]
    public class WeaponSO : StaticItemSO
    {
        [SerializeField] private Vector2Int _levelRange = new Vector2Int(1, 10);
        [SerializeField] private int _level = 1;

        private int _dynamicID;

        public override int DynamicID => _dynamicID;

        protected override ItemSO OnGetInstance()
        {
            WeaponSO item = Instantiate(this);
            item._level = Random.Range(_levelRange.x, _levelRange.y);
            item._dynamicID = InventoryUtility.GetID();
            return item;
        }
    }
}