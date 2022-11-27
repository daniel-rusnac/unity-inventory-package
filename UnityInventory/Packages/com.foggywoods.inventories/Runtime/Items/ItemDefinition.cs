using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FoggyWoods.Inventories.Items
{
    [CreateAssetMenu(menuName = "Inventories/Item")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        [SerializeField] private List<Object> _properties = new();

        public List<IItemProperty> Properties => (List<IItemProperty>) _properties.Cast<IItemProperty>();

        public string ID => name;
        
        public T GetProperty<T>(string key)
        {
            foreach (IItemProperty p in Properties)
            {
                if (string.Equals(p.Key, key) && p.Type is T castedProperty)
                    return castedProperty;
            }

            return default;
        }

        public bool TryGetProperty<T>(string key, out T property)
        {
            foreach (IItemProperty p in Properties)
            {
                if (string.Equals(p.Key, key) && p.Type is T castedProperty)
                {
                    property = castedProperty;
                    return true;
                }
            }

            property = default;
            return false;
        }
    }
}