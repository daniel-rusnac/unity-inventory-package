using System.Collections.Generic;
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
        private const string SAVE_KEY = "debug_inventory";

        private bool _drawContent;
        private bool _drawEditor;
        private long _amount = 1;
        private ItemSO _item;
        private InventorySO _inventory;
        private readonly List<InventoryContent> _contentHolders = new List<InventoryContent>();

        private void OnEnable()
        {
            _inventory = (InventorySO) target;
            _inventory.Register(RefreshContent);
            Load();
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
                DrawContent();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawContent()
        {
            if (_contentHolders.Count == 0)
                return;

            int drawn = 0;
            int columns = 5;

            while (drawn < _contentHolders.Count)
            {
                EditorGUILayout.BeginHorizontal();
                
                for (int i = 0; i < columns; i++)
                {
                    DrawContent(_contentHolders[drawn]);

                    drawn++;
                    
                    if (drawn >= _contentHolders.Count)
                        break;
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }

        private void RefreshContent()
        {
            ItemSO[] items = _inventory.GetInstances();
            _contentHolders.Clear();

            foreach (ItemSO item in items)
            {
                InventoryContent content = new InventoryContent();
                content.Initialize(item, _inventory.GetAmount(item), _inventory.GetLimit(item));
                _contentHolders.Add(content);
            }
        }

        private void DrawContent(InventoryContent content)
        {
            EditorGUILayout.BeginVertical("box");
            {
                string label = content.Item.Name;
                if (content.Item.IsInstance)
                {
                    label += " (Instance)";
                }
                EditorGUILayout.LabelField(label);

                string amount = content.Amount.ToString();
                if (content.Limit != -1)
                {
                    amount += $" / {content.Limit}";
                }
                EditorGUILayout.LabelField(amount);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawInventoryEditor()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Inventory modification available only at runtime!", MessageType.Warning);
            }

            GUI.enabled = Application.isPlaying;

            _item = (ItemSO) EditorGUILayout.ObjectField(_item, typeof(ItemSO), _item);
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
                        _inventory.AddAmount(_item, _amount);
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
                        _inventory.RemoveAmount(_item, _amount);
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
                    _inventory.Save(SAVE_KEY);
                }

                if (GUILayout.Button("Load"))
                {
                    _inventory.Load(SAVE_KEY);
                }
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox($"Default save key is '{SAVE_KEY}'.", MessageType.Info);
            }
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