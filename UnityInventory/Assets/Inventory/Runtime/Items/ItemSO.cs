using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemSO : ScriptableObject
    {
        private bool _isInstance;
        
        public bool IsInstance => _isInstance;
        public bool IsDynamic => StaticID != DynamicID;
        
        public abstract int StaticID { get; }
        public abstract int DynamicID { get; }
        public abstract string Name { get; }

        public ItemSO GetInstance()
        {
            ItemSO item = OnGetInstance();
            item._isInstance = true;

            if (item.IsDynamic)
            {
                InventoryUtility.AddItemToDatabase(item);
            }
            
            return item;
        }

        public string Serialize()
        {
            return OnSerialize();
        }

        public virtual ItemSO Deserialize(string data)
        {
            return GetInstance();
        }

        protected virtual string OnSerialize()
        {
            return "";
        }

        protected abstract ItemSO OnGetInstance();
    }
}