using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomPropertyDrawer(typeof(Inventory))]
    public class InventoryDrawer : PropertyDrawer
    {
        private const float BUTTON_WIDTH = 60f;

        private static int _intInput = 100;
        private static bool _isShown;
        private static ItemSO _item;
        private static Inventory _inventory;

        private float _additionalHeight;
        private bool _isInitialized;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_isInitialized)
            {
                Initialize(property);
            }

            Vector2 verticalOffset = Vector2.up * (EditorGUIUtility.singleLineHeight +
                                                   EditorGUIUtility.standardVerticalSpacing);
            _additionalHeight = 0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                position.height = EditorGUIUtility.singleLineHeight;
                _isShown = EditorGUI.BeginFoldoutHeaderGroup(position, _isShown, label);
                if (_isShown)
                {
                    GUI.enabled = Application.isPlaying;

                    // draw item
                    Rect rectContent = new Rect(position.position + verticalOffset,
                        new Vector2(position.size.x, EditorGUIUtility.singleLineHeight));
                    EditorGUI.LabelField(rectContent, _inventory.ToString());
                    rectContent.position += verticalOffset;
                    _item = (ItemSO) EditorGUI.ObjectField(rectContent, _item, typeof(ItemSO), _item);

                    // draw int input 
                    rectContent.position += verticalOffset;
                    _intInput = EditorGUI.IntField(new Rect(rectContent) {width = rectContent.width - BUTTON_WIDTH * 3},
                        _intInput);

                    // draw buttons
                    if (GUI.Button(
                            new Rect(rectContent) {width = BUTTON_WIDTH, x = rectContent.xMax - BUTTON_WIDTH * 3},
                            "Add"))
                    {
                        _inventory.Add(_item, _intInput);
                    }

                    if (GUI.Button(
                            new Rect(rectContent) {width = BUTTON_WIDTH, x = rectContent.xMax - BUTTON_WIDTH * 2},
                            "Remove"))
                    {
                        _inventory.Remove(_item, _intInput);
                    }

                    if (GUI.Button(new Rect(rectContent) {width = BUTTON_WIDTH, x = rectContent.xMax - BUTTON_WIDTH},
                            "Set Max"))
                    {
                        _inventory.SetMax(_item, _intInput);
                    }

                    GUI.enabled = true;

                    DrawRuntimeWarning(ref rectContent);
                    DrawSetMaxHint(ref rectContent);
                }

                EditorGUI.EndFoldoutHeaderGroup();
            }
            EditorGUI.EndProperty();
        }

        private void DrawRuntimeWarning(ref Rect position)
        {
            if (Application.isPlaying)
                return;
            
            EditorGUI.HelpBox(new Rect(position)
                {
                    height = EditorGUIUtility.singleLineHeight * 2,
                    y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                },
                "Inventory modification available only at runtime!", MessageType.Warning);

            _additionalHeight += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
            position.y += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }

        private void DrawSetMaxHint(ref Rect position)
        {
            if (!Application.isPlaying)
                return;
            
            EditorGUI.HelpBox(new Rect(position)
                {
                    height = EditorGUIUtility.singleLineHeight * 2,
                    y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                },
                "Set max to -1 to ignore it.", MessageType.Info);

            _additionalHeight += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
            position.y += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (_isShown)
            {
                height += EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 3;
                height += _additionalHeight;
            }

            return height;
        }

        private void Initialize(SerializedProperty property)
        {
            _isInitialized = true;
            _inventory = fieldInfo.GetValue(property.serializedObject.targetObject) as Inventory;
        }
    }
}