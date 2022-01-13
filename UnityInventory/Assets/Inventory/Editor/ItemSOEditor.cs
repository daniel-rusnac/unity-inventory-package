using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(ItemSO), true)]
    public class ItemSOEditor : Editor
    {
        private ItemSO item;
        private SerializedProperty normalIconProperty;
        private SerializedProperty lockedIconProperty;
        private float IconSize => EditorGUIUtility.standardVerticalSpacing * 3 + EditorGUIUtility.singleLineHeight * 3;

        protected virtual void OnEnable()
        {
            item = (ItemSO)target;
            normalIconProperty = serializedObject.FindProperty("normalIcon");
            lockedIconProperty = serializedObject.FindProperty("lockedIcon");
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
            normalIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(normalIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));

            lockedIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(lockedIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}