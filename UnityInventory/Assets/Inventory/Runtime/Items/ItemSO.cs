using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemSO : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _icon;

        public bool IsDynamic => StaticID != DynamicID;
        public int StaticID => _id;
        public virtual int DynamicID => StaticID;
        public virtual string Glyph => _glyph;
        public virtual Sprite Icon => _icon;
        public virtual string Name => _name;
        public bool IsInstance { get; protected set; }

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

        public virtual string GetDebugString()
        {
            return "";
        }

        protected abstract ItemSO OnGetInstance();
    }
}