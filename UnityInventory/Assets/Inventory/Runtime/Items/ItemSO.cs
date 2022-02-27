using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemSO : ScriptableObject
    {
        public bool IsInstance { get; protected set; }
        public bool IsDynamic => StaticID != DynamicID;
        
        public abstract int StaticID { get; }
        public abstract int DynamicID { get; }
        public abstract string Name { get; }

        public ItemSO GetInstance()
        {
            ItemSO item = OnGetInstance();
            item.IsInstance = true;

            if (item.IsDynamic)
            {
                InventoryUtility.AddItemToDatabase(item);
            }
            
            return item;
        }

        public virtual object Serialize()
        {
            return default;
        }

        public virtual ItemSO Deserialize(int dynamicId, object data)
        {
            return GetInstance();
        }

        protected abstract ItemSO OnGetInstance();
    }
}