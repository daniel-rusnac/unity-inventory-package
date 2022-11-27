using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace FoggyWoods.Inventories
{
    public class ItemPropertyElement : VisualElement
    {
        public ItemPropertyElement(Object propertyObject, Action onRemove)
        {
            SerializedObject serializedObject = new SerializedObject(propertyObject);

            VisualTreeAsset visualTreeAsset = AssetDatabaseUtility.LoadAssetRelativeToScript
                <ItemPropertyElement, VisualTreeAsset>("UI/ItemPropertyUxml.uxml");

            visualTreeAsset.CloneTree(this);

            this.Q<TextField>("key").BindProperty(serializedObject.FindProperty("_key"));
            this.Q<Button>("delete-button").clicked += onRemove;
            this.Q<PropertyField>("value").BindProperty(serializedObject.FindProperty("_value"));
        }
    }
}