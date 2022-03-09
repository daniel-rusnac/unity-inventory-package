using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    internal class InventoryContent
    {
        public long Amount { get; private set; }
        public long Limit { get; private set; }
        public ItemSO Item { get; private set; }

        public void Initialize(ItemSO item, long amount, long limit)
        {
            Item = item;
            Amount = amount;
            Limit = limit;
        }
    }

    [CustomEditor(typeof(InventorySO), true)]
    public class InventorySOEditor : Editor
    {
        private const string DRAW_CONTENT_KEY = "ie_draw_content";
        private const string DRAW_EDITOR_KEY = "ie_draw_editor";
        private const string AMOUNT_KEY = "ie_amount";
        private const string ITEM_ID_KEY = "ie_item_id";
        private const float SLOT_WIDTH = 100;

        private string _saveKey;
        private bool _drawContent;
        private bool _drawEditor;
        private long _amount = 1;
        private ItemSO _item;
        private InventorySO _inventory;
        private readonly List<InventoryContent> _contentHolders = new List<InventoryContent>();

        private void OnEnable()
        {
            _inventory = (InventorySO)target;
            _saveKey = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(target)).ToString() + "_inventory";
            _inventory.Register(RefreshContent);
            Load();

            if (Application.isPlaying)
            {
                RefreshContent();
            }
        }

        private void OnDisable()
        {
            _inventory.Unregister(RefreshContent);
            Save();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!InventoryUtility.IsInitialized)
            {
                EditorGUILayout.HelpBox("Inventory database is not initialized! Editing is unavailable.",
                    MessageType.Warning);
                return;
            }

            _drawEditor = EditorGUILayout.BeginFoldoutHeaderGroup(_drawEditor, "Editor");
            if (_drawEditor)
            {
                DrawInventoryEditor();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            _drawContent = EditorGUILayout.BeginFoldoutHeaderGroup(_drawContent, "Content");
            if (_drawContent)
            {
                EditorGUILayout.HelpBox("Click an item to display it in the inspector.", MessageType.Info);
                DrawContent();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawContent()
        {
            if (_contentHolders.Count == 0)
                return;

            int drawn = 0;
            int columns = (int)((Screen.width + EditorGUIUtility.standardVerticalSpacing - SLOT_WIDTH * 0.5f) /
                                (SLOT_WIDTH + EditorGUIUtility.standardVerticalSpacing));

            while (drawn < _contentHolders.Count)
            {
                EditorGUILayout.BeginHorizontal();

                for (int i = 0; i < columns; i++)
                {
                    DrawContent(_contentHolders[drawn], i == columns - 1);

                    drawn++;

                    if (drawn >= _contentHolders.Count)
                        break;
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void RefreshContent()
        {
            ItemSO[] items = _inventory.GetInstances().OrderBy(so => so.IsDynamic).ThenBy(so => so.Name).ToArray();
            _contentHolders.Clear();

            foreach (ItemSO item in items)
            {
                InventoryContent content = new InventoryContent();
                content.Initialize(item, _inventory.GetAmount(item), _inventory.GetLimit(item));
                _contentHolders.Add(content);
            }

            Repaint();
        }

        private void DrawContent(InventoryContent content, bool expand)
        {
            EditorGUILayout.BeginVertical("box", GUILayout.MinWidth(SLOT_WIDTH),
                GUILayout.MaxWidth(expand ? Screen.width : SLOT_WIDTH));
            {
                string label = content.Item.Name;
                if (content.Item.IsDynamic)
                {
                    label += " (Dynamic)";
                }

                EditorGUILayout.LabelField(label, GUILayout.MinWidth(SLOT_WIDTH));

                string amount = content.Amount.ToString();
                if (content.Limit != -1)
                {
                    amount += $" / {content.Limit}";
                }

                EditorGUILayout.LabelField(amount, GUILayout.MinWidth(SLOT_WIDTH));
            }
            EditorGUILayout.EndVertical();

            Rect rect = GUILayoutUtility.GetLastRect();

            if (GUI.Button(rect, "", GUIStyle.none))
            {
                Selection.SetActiveObjectWithContext(content.Item, content.Item);
            }
        }

        private void DrawInventoryEditor()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Inventory modification available only at runtime!", MessageType.Warning);
            }

            GUI.enabled = Application.isPlaying;

            _item = (ItemSO)EditorGUILayout.ObjectField(_item, typeof(ItemSO), _item);
            _amount = EditorGUILayout.LongField(_amount);

            EditorGUILayout.BeginHorizontal();
            {
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

                if (GUILayout.Button("Set Amount"))
                {
                    if (_item == null)
                    {
                        Debug.LogWarning("No item selected!");
                    }
                    else
                    {
                        _inventory.SetAmount(_item, _amount);
                    }
                }

                long limit = _item == null ? -1 : _inventory.GetLimit(_item);

                if (limit == -1)
                {
                    if (GUILayout.Button("Set Limit"))
                    {
                        if (_item == null)
                        {
                            Debug.LogWarning("No item selected!");
                        }
                        else
                        {
                            _inventory.SetLimit(_item, _amount);
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button("Remove Limit"))
                    {
                        if (_item == null)
                        {
                            Debug.LogWarning("No item selected!");
                        }
                        else
                        {
                            _inventory.SetLimit(_item, -1);
                        }
                    }

                    RefreshContent();
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                    _inventory.Save(_saveKey);
                }

                if (GUILayout.Button("Load"))
                {
                    _inventory.Load(_saveKey);
                }
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        private void Save()
        {
            EditorPrefs.SetBool(DRAW_CONTENT_KEY, _drawContent);
            EditorPrefs.SetBool(DRAW_EDITOR_KEY, _drawEditor);
            EditorPrefs.SetString(AMOUNT_KEY, _amount.ToString());

            if (_item != null)
            {
                EditorPrefs.SetInt(ITEM_ID_KEY, _item.StaticID);
            }
        }

        private void Load()
        {
            _drawContent = EditorPrefs.GetBool(DRAW_CONTENT_KEY, true);
            _drawEditor = EditorPrefs.GetBool(DRAW_EDITOR_KEY, true);
            _amount = long.Parse(EditorPrefs.GetString(AMOUNT_KEY, "1"));

            int itemID = EditorPrefs.GetInt(ITEM_ID_KEY);

            if (InventoryUtility.TryGetItem(itemID, out ItemSO item))
            {
                _item = item;
            }
        }
    }
}