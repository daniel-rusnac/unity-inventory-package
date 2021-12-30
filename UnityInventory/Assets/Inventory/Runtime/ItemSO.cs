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
        [SerializeField] private Sprite icon;

        public byte ID => id;
        public string ItemName => itemName;
        public string Glyph => glyph;
        public Sprite Icon => icon;
    }
}