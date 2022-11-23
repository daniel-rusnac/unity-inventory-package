using System.Linq;
using Items.Modules;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Items
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        private Item _item;
        private VisualElement _modulesElement;

        private void OnEnable()
        {
            _item = (Item) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            _modulesElement = new VisualElement();
            RefreshModules();
            
            root.Add(new PropertyField(serializedObject.FindProperty("_id")));
            root.Add(_modulesElement);
            root.Add(new Button(OnAddModuleClicked) {text = "Add"});
            root.Add(new Button(OnRemoveModuleClicked) {text = "Remove"});
            return root;
        }

        private void OnAddModuleClicked()
        {
            IntItemModule module = CreateInstance<IntItemModule>();
            _item.AddModule(module);
            AssetDatabase.AddObjectToAsset(module, _item);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);
            
            RefreshModules();
        }

        private void OnRemoveModuleClicked()
        {
            if (!_item.Modules.Any())
                return;

            ItemModule module = _item.Modules.Last();
            _item.RemoveModule(module);
            AssetDatabase.RemoveObjectFromAsset(module);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);
            
            RefreshModules();
        }

        private void RefreshModules()
        {
            int childCount = _modulesElement.childCount;

            for (int i = childCount - 1; i >= 0; i--)
                _modulesElement.RemoveAt(i);

            foreach (ItemModule module in _item.Modules)
            {
                _modulesElement.Add(new InspectorElement(module));
            }
        }
    }
}