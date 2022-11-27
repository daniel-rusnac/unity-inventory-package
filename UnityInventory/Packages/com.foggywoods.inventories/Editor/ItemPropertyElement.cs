using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace FoggyWoods.Inventories
{
    public class ItemPropertyElement : VisualElement
    {
        public ItemPropertyElement(Object propertyObject, Action onRemove)
        {
            Add(new InspectorElement(propertyObject));
            Add(new Button(onRemove) {text = "Delete"});
        }
    }
}