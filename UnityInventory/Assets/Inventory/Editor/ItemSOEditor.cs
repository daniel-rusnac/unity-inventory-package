using InventorySystem.New;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CustomEditor(typeof(StaticItemSO), true)]
    public class ItemSOEditor : Editor
    {
        private SerializedProperty _iconProperty;
        private SerializedProperty _nameProperty;
        private SerializedProperty _idProperty;
        private SerializedProperty _glyphProperty;
        private GUIStyle _style;
        private bool _isInitialized;
        private float IconSize => EditorGUIUtility.standardVerticalSpacing * 3 + EditorGUIUtility.singleLineHeight * 3;

        protected virtual void OnEnable()
        {
            _iconProperty = serializedObject.FindProperty("_icon");
            _nameProperty = serializedObject.FindProperty("_name");
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

            DrawPropertiesExcluding(serializedObject, "m_Script", "_name", "_id", "_glyph", "_icon");
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
            _iconProperty.objectReferenceValue = EditorGUILayout.ObjectField(
                _iconProperty.objectReferenceValue,
                typeof(Sprite), false, GUILayout.Width(IconSize), GUILayout.Height(IconSize));

            Rect rect = GUILayoutUtility.GetLastRect();
            EditorGUI.LabelField(rect, "Normal", _style);

            serializedObject.ApplyModifiedProperties();
        }
    }
}