
namespace CraftsPeople.Data
{
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "Text-", menuName = "ScriptableObjects/DisplayableText")]
    public class DisplayableText : ScriptableObject
    {
        public bool HasAuthor;
        public Author Author;

        [TextArea]
        public string DisplayText;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DisplayableText))]
    public class DisplayableTextEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var authorProp = serializedObject.FindProperty("Author");
            var hasAuthorProp = serializedObject.FindProperty("HasAuthor");
            var displayTextProp = serializedObject.FindProperty("DisplayText");

            serializedObject.Update();

            EditorGUILayout.PropertyField(hasAuthorProp);
            GUI.enabled = hasAuthorProp.boolValue;
            EditorGUILayout.PropertyField(authorProp);
            GUI.enabled = true;
            EditorGUILayout.PropertyField(displayTextProp);

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
