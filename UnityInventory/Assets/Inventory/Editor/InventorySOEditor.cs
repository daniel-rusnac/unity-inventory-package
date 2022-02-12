using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(InventorySO))]
    public class InventorySOEditor : Editor
    {
        private const string DRAW_CONTENT_KEY = "ie_draw_content";
        private const string DRAW_EDITOR_KEY = "ie_draw_editor";
        private const string AMOUNT_KEY = "ie_amount";
        private const string ITEM_ID_KEY = "ie_item_id";

        private bool _drawContent;
        private bool _drawEditor;
        private int _amount = 1;
        private ItemSO _item;
        private SimpleInventorySO _inventory;

        private void OnEnable()
        {
            _inventory = (SimpleInventorySO) target;
            Load();
        }

        private void OnDisable()
        {
            Save();
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
            _amount = EditorGUILayout.IntField(_amount);

            if (GUILayout.Button("Add"))
            {
                if (_item == null)
                {
                    Debug.LogWarning("No item selected!");
                }
                else
                {
                    _inventory.Add(_item, _amount);
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
                    _inventory.Remove(_item, _amount);
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
                    _inventory.SetMax(_item, _amount);
                }
            }

            GUI.enabled = true;

            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Set max to -1 to ignore it.", MessageType.Info);
            }
        }

        private void Save()
        {
            EditorPrefs.SetBool(DRAW_CONTENT_KEY, _drawContent);
            EditorPrefs.SetBool(DRAW_EDITOR_KEY, _drawEditor);
            EditorPrefs.SetInt(AMOUNT_KEY, _amount);

            if (_item != null)
            {
                EditorPrefs.SetInt(ITEM_ID_KEY, _item.ID);
            }
        }

        private void Load()
        {
            _drawContent = EditorPrefs.GetBool(DRAW_CONTENT_KEY, true);
            _drawEditor = EditorPrefs.GetBool(DRAW_EDITOR_KEY, true);
            _amount = EditorPrefs.GetInt(AMOUNT_KEY, 1);

            int itemID = EditorPrefs.GetInt(ITEM_ID_KEY);

            if (InventoryUtility.TryGetItem(itemID, out ItemSO item))
            {
                _item = item;
            }
        }
    }
}