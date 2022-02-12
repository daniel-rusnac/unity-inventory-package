using UnityEngine;

namespace InventorySystem.Icons
{
    [CreateAssetMenu(fileName = "Simple Icon", menuName = InventoryConstants.CREATE_ICONS_SM + "Simple Icon")]
    public class SimpleIcon : IconSO
    {
        [SerializeField] private Sprite _normalIcon;
        [SerializeField] private Sprite _lockedIcon;
        
        public override Sprite GetIcon()
        {
            return GetIcon(IconType.Normal);
        }

        public override Sprite GetIcon<T>(T type)
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