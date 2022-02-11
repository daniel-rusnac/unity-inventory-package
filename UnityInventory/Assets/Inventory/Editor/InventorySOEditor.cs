using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(InventorySO))]
    public class InventorySOEditor : Editor
    {
        private const float BUTTON_WIDTH = 60f;

        private bool _drawContent;
        private bool _drawEditor;
        private int _intInput = 100;
        private ItemSO _item;
        private InventorySO _inventory;

        private void OnEnable()
        {
            _inventory = (InventorySO) target;
        }

        public override void OnInspectorGUI()
        {
            _drawContent = EditorGUILayout.BeginFoldoutHeaderGroup(_drawContent, "Content");
            if (_drawContent)
            {
                DrawContent();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            _drawEditor = EditorGUILayout.BeginFoldoutHeaderGroup(_drawEditor, "Editor");
            if (_drawEditor)
            {
                DrawInventoryEditor();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawContent()
        {
            EditorGUILayout.LabelField(_inventory.ToString());
        }

        private void DrawInventoryEditor()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Inventory modification available only at runtime!", MessageType.Warning);
            }
            
            GUI.enabled = Application.isPlaying;
            
            _item = (ItemSO) EditorGUILayout.ObjectField(_item, typeof(ItemSO), _item);
            _intInput = EditorGUILayout.IntField(_intInput);

            if (GUILayout.Button("Add"))
            {
                if (_item == null)
                {
                    Debug.LogWarning("No item selected!");
                }
                else
                {
                    _inventory.Add(_item, _intInput);
                }
            }

            if (GUILayout.Button("Remove"))
            {
                if (_item == null)
                {
                    Debug.LogWarning("No item selected!");
                }
                else
                {
                    _inventory.Remove(_item, _intInput);
                }
            }

            if (GUILayout.Button("Set Max"))
            {
                if (_item == null)
                {
                    Debug.LogWarning("No item selected!");
                }
                else
                {
                    _inventory.SetMax(_item, _intInput);
                }
            }

            GUI.enabled = true;

            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Set max to -1 to ignore it.", MessageType.Info);
            }
        }
    }
}