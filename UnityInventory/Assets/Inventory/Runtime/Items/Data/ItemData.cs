using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemData<T> : ItemDataBase
    {
        [SerializeField] private T _value;

        public T GetValue()
        {
            return _value;
        }

        public override TData GetValue<TData>()
        {
            return GetValue() as TData;
        }
    }
}