using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private string id;
        [Tooltip("Will be used to display the name in the UI.")]
        [SerializeField] private string itemName;
        [SerializeField] private string glyph;
        [SerializeField] private Sprite icon;

        public string ID => id;
        public string ItemName => itemName;
        public string Glyph => glyph;
        public Sprite Icon => icon;
    }
}