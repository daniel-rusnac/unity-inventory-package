using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FoggyWoods.Inventories.Properties
{
    public abstract class ItemProperty<T> : ScriptableObject, IItemProperty
    {
        [SerializeField] private string _key;
        [SerializeField] private T _value;

        public string Key => _key;

        public object Value => _value;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (string.Equals(name, _key))
                return;

            name = _key;
            EditorUtility.SetDirty(this);
#endif
        }
    }
}