using UnityEngine;

namespace Items.Modules
{
    public abstract class ValueItemModule<T> : ItemModule
    {
        [field: SerializeField] public T Value { get; private set; }
    }
}