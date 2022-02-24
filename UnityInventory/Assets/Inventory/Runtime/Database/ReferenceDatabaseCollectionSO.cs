using UnityEngine;

namespace InventorySystem.Database
{
    [CreateAssetMenu(fileName = "Manual Collection", menuName = InventoryConstants.CREATE_DATABASE_SUB_MENU + "Manual Collection")]
    public class ReferenceDatabaseCollectionSO : ScriptableObject
    {
        [SerializeField] protected ItemSO[] _items;

        public ItemSO[] Items => _items;
    }
}