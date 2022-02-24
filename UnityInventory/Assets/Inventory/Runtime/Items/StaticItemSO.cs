using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Static Item", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Static Item")]
    public class StaticItemSO : ItemSO
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _icon;

        public override int StaticID => _id;
        public virtual string Glyph => _glyph;
        public virtual Sprite Icon => _icon;

        public override int DynamicID => StaticID;
        public override string Name => _name;

        protected override ItemSO OnGetInstance()
        {
            return this;
        }
    }
}