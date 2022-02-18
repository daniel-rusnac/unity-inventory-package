using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemDataBase : ScriptableObject
    {
        public abstract T GetValue<T>() where T : class;
    }
}