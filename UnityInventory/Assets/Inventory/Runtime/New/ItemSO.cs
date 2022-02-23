using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemSO : ScriptableObject
    {
        public bool IsDynamic => ID != DynamicID;
        
        public abstract int ID { get; }
        public abstract int DynamicID { get; }
        public abstract string Name { get; }
        public abstract ItemSO GetInstance();

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
    }
}