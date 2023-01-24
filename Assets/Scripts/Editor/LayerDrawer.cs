using OmniDi.Library.Wrappers;
using UnityEditor;
using UnityEngine;

namespace OmniDi.Library.Editor
{
    [CustomPropertyDrawer(typeof(Layer))]
    public class LayerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var layerProperty = property.FindPropertyRelative("layer");
            layerProperty.intValue = EditorGUI.LayerField(position, label, layerProperty.intValue);
        }
    }
}