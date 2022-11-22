using UnityEngine;

namespace Items.Modules
{
    public abstract class ValueItemModule<T> : ItemModuleBase
    {
        [field: SerializeField] public T Value { get; private set; }
    }
}