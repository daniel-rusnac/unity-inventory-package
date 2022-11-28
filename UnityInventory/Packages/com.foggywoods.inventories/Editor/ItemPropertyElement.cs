using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FoggyWoods.Inventories
{
    public class ItemPropertyElement : VisualElement
    {
        private Action<ScriptableObject> _onRemove;
        private ScriptableObject _propertyObject;

        public ItemPropertyElement(ScriptableObject propertyObject, Action<ScriptableObject> onRemove)
        {
            _propertyObject = propertyObject;
            _onRemove = onRemove;
            TemplateContainer propertyElement = ResourcesProvider.ItemPropertyUxml.Instantiate();
            Add(propertyElement);
            propertyElement.Bind(new SerializedObject(propertyObject));
            
            this.Q<Button>("delete-button").clicked += OnRemoveClicked;
        }

        private void OnRemoveClicked()
        {
            _onRemove?.Invoke(_propertyObject);
        }
    }
}