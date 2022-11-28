using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FoggyWoods.Inventories
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition _item;
        private VisualElement _content;
        private Dictionary<ScriptableObject, VisualElement> _elementByProperty = new Dictionary<ScriptableObject, VisualElement>();

        private void OnEnable()
        {
            _item = (ItemDefinition) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualTreeAsset visualTree = ResourcesProvider.ItemDefinitionUxml;
            VisualElement root = visualTree.CloneTree();

            _content = root.Q<VisualElement>("content");
            root.Q<Label>("id").text = _item.ID;
            root.Q<Button>("add-button").clicked += OnAddPropertyClicked;

            RefreshProperties();
            
            return root;
        }

        private void OnAddPropertyClicked()
        {
            Rect rect = new Rect(Event.current.mousePosition, new Vector2(0, 0));
            ItemPropertiesDropDown dropdown = new ItemPropertiesDropDown(AddProperty);
            dropdown.Show(rect);
        }

        private void AddProperty(Type type)
        {
            AddProperty(CreateInstance(type));
        }
        private void AddProperty(ScriptableObject property)
        {
            _item.Properties.Add(property);
            AssetDatabase.AddObjectToAsset(property, _item);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            // RefreshProperties();
        }

        private void RemoveProperty(ScriptableObject property)
        {
            _item.Properties.Remove(property);
            AssetDatabase.RemoveObjectFromAsset(property);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            // RefreshProperties();
        }

        private void RefreshProperties()
        {
            // int childCount = _content.childCount;

            // for (int i = childCount - 1; i >= 0; i--)
                // _content.RemoveAt(i);

                foreach (ScriptableObject property in _item.Properties)
                {
                    _content.Add(new ItemPropertyElement(property, RemoveProperty));
                    // _content.Add(new InspectorElement(property));
                }        
        }
    }
}