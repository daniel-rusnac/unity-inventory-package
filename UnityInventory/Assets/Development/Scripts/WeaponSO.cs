using InventorySystem;
using UnityEngine;

namespace Development
{
    public class WeaponData : IDynamicData
    {
        public readonly int Level;
        
        public WeaponData(int level)
        {
            Level = level;
        }
    }
    
    [CreateAssetMenu(fileName = "Weapon", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Weapon")]
    public class WeaponSO : ItemSO, IDynamicItem<WeaponData>
    {
        [SerializeField] private int _damagePerLevel = 10;
        [SerializeField] private Vector2Int _levelRange = new Vector2Int(1, 10);

        public WeaponData CreateDataInstance => new WeaponData(Random.Range(_levelRange.x, _levelRange.y));

        public int GetDamage(WeaponData data)
        {
            return data.Level * _damagePerLevel;
        }
    }
}