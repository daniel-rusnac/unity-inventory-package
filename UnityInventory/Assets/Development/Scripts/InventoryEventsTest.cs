using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    public class InventoryEventsTest : MonoBehaviour
    {
        [SerializeField] private WeaponSO _weapon;
        [SerializeField] private DynamicInventorySO _inventory;

        private void OnEnable()
        {
            _inventory.Register(OnChanged);
            _inventory.Register(OnChangedDelta);
        }

        private void OnDisable()
        {
            _inventory.Unregister(OnChanged);
            _inventory.Unregister(OnChangedDelta);
        }

        private void OnChanged()
        {
            Debug.Log("Changed");
            _inventory.Add(_weapon, 1);
        }

        private void OnChangedDelta(ItemSO item, long delta)
        {
            Debug.Log($"Changed: {item.ItemName} {delta}");
        }
    }
}