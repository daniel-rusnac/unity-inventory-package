using InventorySystem;
using UnityEngine;

namespace Development.Scripts
{
    public class FeatureTester : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventorySO;
        [SerializeField] private ItemSO[] _itemSO; 

        [ContextMenu("Test")]
        private void Test()
        {
            _itemSO = _inventorySO.GetInventory.GetAllItems();
        }
    }
}