using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoggyWoods.Inventories
{
    [CreateAssetMenu(menuName = "Inventories/Property ID Database", fileName = "propertyIdDatabase")]
    public class PropertyIdDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private PropertyNameID[] _propertyNames = Array.Empty<PropertyNameID>();

        private static Dictionary<int, string> s_nameById = new Dictionary<int, string>();

        public static string GetPropertyName(int id)
        {
            return s_nameById.ContainsKey(id) 
                ? string.Empty 
                : s_nameById[id];
        }

        public void OnBeforeSerialize()
        {
            _propertyNames = s_nameById.Select(pair => new PropertyNameID(pair.Key, pair.Value)).ToArray();
        }

        public void OnAfterDeserialize()
        {
            s_nameById = _propertyNames.ToDictionary(propertyName => propertyName.ID, propertyName => propertyName.Name);
        }

        [Serializable]
        private struct PropertyNameID
        {
            public int ID;
            public string Name;

            public PropertyNameID(int id, string name)
            {
                ID = id;
                Name = name;
            }
        }
    }
}