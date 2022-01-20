using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(ItemSO), true)]
    public class ItemSOEditor : Editor
    {
        private ItemSO _item;
        private SerializedProperty _normalIconProperty;
        private SerializedProperty _lockedIconProperty;
        private float IconSize => EditorGUIUtility.standardVerticalSpacing * 3 + EditorGUIUtility.singleLineHeight * 3;

        protected virtual void OnEnable()
        {
            _item = (ItemSO)target;
            _normalIconProperty = serializedObject.FindProperty("normalIcon");
            _lockedIconProperty = serializedObject.FindProperty("lockedIcon");
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            {
                DrawBasicData();
                DrawIcon();
            }
            GUILayout.EndHorizontal();

            DrawPropertiesExcluding(serializedObject, new[] { "m_Script", "itemName", "id", "glyph", "normalIcon",  "lockedIcon" });
        }

        private void DrawBasicData()
        {
            GUILayout.BeginVertical();
            {
                EditorGUIUtility.labelWidth = 75;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("itemName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("glyph"));
                EditorGUIUtility.labelWidth = 0;
            }
            GUILayout.EndVertical();
        }

        private void DrawIcon()
        {
            _normalIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(_normalIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));

            _lockedIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(_lockedIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}