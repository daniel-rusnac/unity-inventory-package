using UnityEngine;

namespace ItemManagement.Items
{
    [CreateAssetMenu(menuName = "Item Management/Items/Item", fileName = "New Item")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        [SerializeField] private string _id = "unknown";
        [SerializeField] private bool _isStackable = true;

        public string Id => _id;
        public bool IsStackable => _isStackable;
    }
}