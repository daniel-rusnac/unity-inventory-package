﻿using System.Collections.Generic;
using UnityEngine;

namespace FoggyWoods.Inventories.Items
{
    [CreateAssetMenu(menuName = "Inventories/Item")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        [SerializeField] private List<ScriptableObject> _properties = new();

        public List<ScriptableObject> Properties => _properties;

        public string ID => name;

        public T GetProperty<T>(string key)
        {
            TryGetProperty(key, out T property);
            return property;
        }

        public bool TryGetProperty<T>(string key, out T property)
        {
            foreach (ScriptableObject so in Properties)
            {
                if (so is IItemProperty itemProperty &&
                    string.Equals(itemProperty.Key, key) &&
                    itemProperty.Type is T castedProperty)
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