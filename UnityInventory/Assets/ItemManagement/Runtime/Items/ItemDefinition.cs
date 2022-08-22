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
        
        public object Clone()
        {
            var instance = Instantiate(this);

            IncrementInstanceCounter(instance);

            return instance;
        }

        private void IncrementInstanceCounter(ItemDefinition instance)
        {
            string[] parts = _id.Split('_');

            if (parts.Length > 0 && int.TryParse(parts[parts.Length - 1], out int counter))
            {
                instance._id += $"_{counter:00}";
            }
            else
            {
                instance._id += "_01";
            }
        }
    }
}