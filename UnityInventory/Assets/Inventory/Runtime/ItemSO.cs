using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [FormerlySerializedAs("id")]
        [SerializeField] private byte _id;
        [Tooltip("Will be used to display the name in the UI.")]
        [FormerlySerializedAs("itemName")]
        [SerializeField] private string _itemName;
        [FormerlySerializedAs("glyph")]
        [SerializeField] private string _glyph;
        [FormerlySerializedAs("normalIcon")]
        [SerializeField] private Sprite _normalIcon;
        [FormerlySerializedAs("lockedIcon")]
        [SerializeField] private Sprite _lockedIcon;

        public byte ID => _id;
        public string ItemName => _itemName;
        public string Glyph => _glyph;
        
        [Obsolete("Use GetIcon() instead.")]
        public Sprite Icon => GetIcon();

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