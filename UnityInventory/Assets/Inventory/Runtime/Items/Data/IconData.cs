using UnityEngine;

namespace InventorySystem
{
    public class IconDataBase : ItemData<Sprite>
    {
        [SerializeField] private Sprite _normal;
        [SerializeField] private Sprite _locked;

        public Sprite GetIcon(IconType type)
        {
            switch (type)
            {
                case IconType.Locked:
                    return _locked;
            }

            return _normal;
        }
    }
}