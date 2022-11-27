using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FoggyWoods.Inventories.Items.Properties
{
    [Serializable]
    public abstract class ItemProperty<T> : ScriptableObject, IItemProperty
    {
        [SerializeField] private string _key;
        [SerializeField] private T _value;

        public string Key => _key;

        public object Value => _value;

        public Type Type => _value.GetType();
    }
}