using System;
using InventorySystem.Icons;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Simple Item", menuName = InventoryConstants.CREATE_ITEMS_SM + "Simple Item")]
    public class ItemSO : ScriptableObject
    {
        [Tooltip("The ID is used by the inventory to store and load items. This must be unique!")]
        [SerializeField] private int _id;
        [Tooltip("Will be used to display the name in the UI.")]
        [SerializeField] private string _itemName;
        [Tooltip("Use with TMP to display images inside text.")]
        [SerializeField] private string _glyph;
        [SerializeField] private IconSO _icon;

        public int ID => _id;
        public string ItemName => _itemName;
        public string Glyph => _glyph;

        protected virtual void Reset()
        {
            RefreshID();
        }
        
        [ContextMenu("Refresh ID")]
        private void RefreshID()
        {
#if UNITY_EDITOR
            _id = Guid.NewGuid().GetHashCode();
            EditorUtility.SetDirty(this);
#endif
        }
    }
}