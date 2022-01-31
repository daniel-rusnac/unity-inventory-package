using System;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(ItemSO), true)]
    public class ItemSOEditor : Editor
    {
        private SerializedProperty _normalIconProperty;
        private SerializedProperty _lockedIconProperty;
        private SerializedProperty _nameProperty;
        private SerializedProperty _idProperty;
        private SerializedProperty _glyphProperty;
        private GUIStyle _style;
        private bool _isInitialized;
        private float IconSize => EditorGUIUtility.standardVerticalSpacing * 3 + EditorGUIUtility.singleLineHeight * 3;

        protected virtual void OnEnable()
        {
            _normalIconProperty = serializedObject.FindProperty("_normalIcon");
            _lockedIconProperty = serializedObject.FindProperty("_lockedIcon");
            _nameProperty = serializedObject.FindProperty("_itemName");
            _idProperty = serializedObject.FindProperty("_id");
            _glyphProperty = serializedObject.FindProperty("_glyph");
        }

        public override void OnInspectorGUI()
        {
            Initialize();

            GUILayout.BeginHorizontal();
            {
                DrawBasicData();
                DrawIcon();
            }
            GUILayout.EndHorizontal();

            DrawPropertiesExcluding(serializedObject, "m_Script", "_itemName", "_id", "_glyph", "_normalIcon", "_lockedIcon");
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            _style = new GUIStyle(EditorStyles.toolbarButton) {alignment = TextAnchor.UpperCenter};
            
            _isInitialized = true;
        }

        private void DrawBasicData()
        {
            GUILayout.BeginVertical();
            {
                EditorGUIUtility.labelWidth = 75;
                EditorGUILayout.PropertyField(_nameProperty);
                EditorGUILayout.PropertyField(_idProperty);
                EditorGUILayout.PropertyField(_glyphProperty);
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