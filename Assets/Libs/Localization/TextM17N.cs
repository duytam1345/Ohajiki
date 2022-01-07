using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualTemplate
{
    [AddComponentMenu("UI/Text(M17N)")]
    public class TextM17N : UnityEngine.UI.Text
    {
        public LocalizeUIData localizeData;
        protected override void Start()
        {
            base.Start();
            if (localizeData != null)
            {
                var data = localizeData.CurrentLocalizeUIData;
                text = data.Text;
                font = data.Font;
            }
        }
    }
#if UNITY_EDITOR
    [CanEditMultipleObjects, CustomEditor(typeof(TextM17N), true)]
    public class TextM17NEditor : UnityEditor.UI.TextEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("localizeData"), true);
            this.serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}