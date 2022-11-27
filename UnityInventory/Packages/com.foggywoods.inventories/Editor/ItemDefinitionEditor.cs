using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FoggyWoods.Inventories
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition _item;
        private VisualElement _propertiesElement;

        private void OnEnable()
        {
            _item = (ItemDefinition) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            _propertiesElement = new VisualElement();
            RefreshProperties();

            root.Add(new Label($"ID: {_item.ID}"));
            root.Add(_propertiesElement);
            root.Add(new Button(OnAddPropertyClicked) {text = "Add"});
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

            RefreshProperties();
        }

        private void RemoveProperty(ScriptableObject property)
        {
            _item.Properties.Remove(property);
            AssetDatabase.RemoveObjectFromAsset(property);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            RefreshProperties();
        }

        private void RefreshProperties()
        {
            int childCount = _propertiesElement.childCount;

            for (int i = childCount - 1; i >= 0; i--)
                _propertiesElement.RemoveAt(i);

            foreach (ScriptableObject property in _item.Properties)
                _propertiesElement.Add(new ItemPropertyElement(property, () => RemoveProperty(property)));
        }
    }
}