using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Simple Item", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Simple Item")]
    public class ItemSO : BaseItemSO
    {
        [Tooltip("Use with TMP to display images inside text.")]
        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _normalIcon;
        [SerializeField] private Sprite _lockedIcon;

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