using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private byte _id;
        [Tooltip("Will be used to display the name in the UI.")] 
        [SerializeField] private string _itemName;
        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _normalIcon;
        [SerializeField] private Sprite _lockedIcon;

        public byte ID => _id;
        public string ItemName => _itemName;
        public string Glyph => _glyph;

        public virtual Sprite GetIcon(IconType type = IconType.Normal)
        {
            switch (type)
            {
                case IconType.Locked:
                    return _lockedIcon;
            }

            return _normalIcon;
        }
    }
}