using UnityEngine;
using UnityEditor;
using OmniDi.Library.Wrappers;

namespace OmniDi.Library.Editor
{
    [CustomPropertyDrawer(typeof(Tag))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tagProperty = property.FindPropertyRelative("tag");
            tagProperty.stringValue = EditorGUI.TagField(position, label, tagProperty.stringValue);
        }
    }
}