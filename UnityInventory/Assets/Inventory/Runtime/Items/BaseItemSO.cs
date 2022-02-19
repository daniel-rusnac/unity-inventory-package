using System;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    public class BaseItemSO : ScriptableObject
    {
        [Tooltip("The ID is used by the inventory to store and load items. This must be unique!")]
        [SerializeField] private int _id;
        [Tooltip("Will be used to display the name in the UI.")]
        [SerializeField] private string _itemName;
        
        public int ID => _id;
        public string ItemName => _itemName;
        
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