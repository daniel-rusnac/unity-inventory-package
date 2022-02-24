using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Weapon")]
    public class WeaponSO : DynamicItemSO
    {
        [SerializeField] private Vector2Int _levelRange = new Vector2Int(1, 10);
        [SerializeField] private int _level = 1;

        protected override void OnInstanceCreated(DynamicItemSO instance)
        {
            WeaponSO weapon = (WeaponSO)instance;
            weapon._level = Random.Range(_levelRange.x, _levelRange.y);
        }

        protected override ItemData OnSerialize()
        {
            return new ItemData();
        }

        protected override void OnDeserialize(ItemData data)
        {
        }
    }
}