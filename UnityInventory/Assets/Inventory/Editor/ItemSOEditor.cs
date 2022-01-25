using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(ItemSO), true)]
    public class ItemSOEditor : Editor
    {
        private SerializedProperty _normalIconProperty;
        private SerializedProperty _lockedIconProperty;
        private GUIStyle _style;
        private float IconSize => EditorGUIUtility.standardVerticalSpacing * 3 + EditorGUIUtility.singleLineHeight * 3;

        protected virtual void OnEnable()
        {
            _normalIconProperty = serializedObject.FindProperty("_normalIcon");
            _lockedIconProperty = serializedObject.FindProperty("_lockedIcon");
            _style = new GUIStyle(EditorStyles.toolbarButton) {alignment = TextAnchor.UpperCenter};
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            {
                DrawBasicData();
                DrawIcon();
            }
            GUILayout.EndHorizontal();

            DrawPropertiesExcluding(serializedObject, "m_Script", "_itemName", "_id", "_glyph", "_normalIcon",
                "_lockedIcon");
        }

        private void DrawBasicData()
        {
            GUILayout.BeginVertical();
            {
                EditorGUIUtility.labelWidth = 75;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_itemName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_id"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_glyph"));
                EditorGUIUtility.labelWidth = 0;
            }
            GUILayout.EndVertical();
        }

        private void DrawIcon()
        {
            _normalIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(
                _normalIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));

            Rect rect = GUILayoutUtility.GetLastRect();
            EditorGUI.LabelField(rect, "Normal", _style);

            _lockedIconProperty.objectReferenceValue = EditorGUILayout.ObjectField(
                _lockedIconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));
            
            rect = GUILayoutUtility.GetLastRect();
            EditorGUI.LabelField(rect, "Locked", _style);

            serializedObject.ApplyModifiedProperties();
        }
    }
}