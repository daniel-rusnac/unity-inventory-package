using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace FoggyWoods.Inventories
{
    internal class ItemPropertiesDropDown : AdvancedDropdown
    {
        private Action<Type> _onSelected;

        public ItemPropertiesDropDown(Action<Type> onSelected) : base(new AdvancedDropdownState())
        {
            _onSelected = onSelected;
            minimumSize = new Vector2(200, 400);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new AdvancedDropdownItem("Properties");
            TypeCache.TypeCollection propertyTypes = TypeCache.GetTypesDerivedFrom<IItemProperty>();

            foreach (Type type in propertyTypes)
            {
                if (!type.IsClass || type.IsAbstract && type.IsGenericType)
                    continue;
                
                root.AddChild(new ItemPropertyDropdownItem(type.Name.Replace("Property", ""), type));
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item is ItemPropertyDropdownItem dropdownItem)
                _onSelected?.Invoke(dropdownItem.Type);
        }
    }
}