using UnityEngine;

namespace Items.Modules
{
    public abstract class ValueItemModule<T> : ItemModule
    {
        [SerializeField] private T _value;

        public T Value => _value;
    }
}