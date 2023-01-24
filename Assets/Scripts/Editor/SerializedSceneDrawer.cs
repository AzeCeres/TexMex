using System.Linq;
using OmniDi.Library.Wrappers;
using UnityEditor;
using UnityEngine;

namespace OmniDi.Library.Editor
{
    [CustomPropertyDrawer(typeof(SerializedScene))]
    public class SerializedSceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var editorBuildScenes = EditorBuildSettings.scenes;
            var sceneNames = editorBuildScenes.Select(editorBuildScene => editorBuildScene.path).ToArray();
            sceneNames = sceneNames.Select(name => name.Remove(0, name.IndexOf('/') + 1)).ToArray();
            sceneNames = sceneNames.Select(name => name.Split('.')[0]).ToArray();

            var sceneProperty = property.FindPropertyRelative("scene");
            sceneProperty.intValue = EditorGUI.Popup(position, label.text, sceneProperty.intValue, sceneNames);
        }
    }
}