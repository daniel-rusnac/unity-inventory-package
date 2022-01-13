using System;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private byte id;
        [Tooltip("Will be used to display the name in the UI.")]
        [SerializeField] private string itemName;
        [SerializeField] private string glyph;
        [SerializeField] private Sprite normalIcon;
        [SerializeField] private Sprite lockedIcon;

        public byte ID => id;
        public string ItemName => itemName;
        public string Glyph => glyph;
        
        [Obsolete("Use GetIcon() instead.")]
        public Sprite Icon => GetIcon();

        public virtual Sprite GetIcon(IconType type = IconType.Normal)
        {
            switch (type)
            {
                case IconType.Locked:
                    return lockedIcon;
            }

            return normalIcon;
        }
    }
}