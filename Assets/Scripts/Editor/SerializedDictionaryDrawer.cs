using OmniDi.Library.Wrappers;
using UnityEditor;
using UnityEngine;

namespace OmniDi.Library.Editor
{
    [CustomPropertyDrawer(typeof(DrawableDictionary), true)]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;

            var keysProp = property.FindPropertyRelative("keys");
            return (keysProp.arraySize + 2) * (EditorGUIUtility.singleLineHeight + 1f);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var isExpanded = property.isExpanded;
            var rect = GetNextRect(ref position);
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);

            if (!isExpanded) return;
            var currentIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = currentIndentLevel + 1;

            var keysProp = property.FindPropertyRelative("keys");
            var valuesProp = property.FindPropertyRelative("values");

            var count = keysProp.arraySize;
            if (valuesProp.arraySize != count) valuesProp.arraySize = count;

            for(int i = 0; i < count; i++)
            {
                rect = GetNextRect(ref position);
                var keyWidth = EditorGUIUtility.labelWidth;
                var valueWidth = rect.width - keyWidth;
                var keyRect = new Rect(rect.xMin, rect.yMin, keyWidth, rect.height);
                var valueRect = new Rect(keyRect.xMax, rect.yMin, valueWidth, rect.height);

                var keyProp = keysProp.GetArrayElementAtIndex(i);
                var valueProp = valuesProp.GetArrayElementAtIndex(i);

                DrawKey(keyRect, keyProp);
                DrawValue(valueRect, valueProp);
            }

            EditorGUI.indentLevel = currentIndentLevel;

            rect = GetNextRect(ref position);
            var plusRect = new Rect(rect.xMax - 60f, rect.yMin, 30f, EditorGUIUtility.singleLineHeight);
            var minusRect = new Rect(rect.xMax - 30f, rect.yMin, 30f, EditorGUIUtility.singleLineHeight);

            if(GUI.Button(plusRect, "+"))
            {
                AddKeyElement(keysProp);
                valuesProp.arraySize = keysProp.arraySize;
            }

            if(GUI.Button(minusRect, "-"))
            {
                keysProp.arraySize = Mathf.Max(keysProp.arraySize - 1, 0);
                valuesProp.arraySize = keysProp.arraySize;
            }
        }

        protected virtual void DrawKey(Rect area, SerializedProperty keyProp)
        {
            EditorGUI.PropertyField(area, keyProp, GUIContent.none, false);
        }

        protected virtual void DrawValue(Rect area, SerializedProperty valueProp)
        {
            EditorGUI.PropertyField(area, valueProp, GUIContent.none, false);
        }




        private static Rect GetNextRect(ref Rect position)
        {
            var rect = new Rect(position.xMin, position.yMin, position.width, EditorGUIUtility.singleLineHeight);
            var height = EditorGUIUtility.singleLineHeight + 1f;
            position = new Rect(position.xMin, position.yMin + height, position.width, position.height = height);
            return rect;
        }


        private static void AddKeyElement(SerializedProperty keysProp)
        {
            keysProp.arraySize++;
            var prop = keysProp.GetArrayElementAtIndex(keysProp.arraySize - 1);

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        var value = 0;
                        for(int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).intValue != value) continue;
                            value++;
                            if (value == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.intValue = value;
                    }
                    break;
                case SerializedPropertyType.Boolean:
                    {
                        var value = false;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).boolValue != value) continue;
                            value = true;
                            break;
                        }
                        prop.boolValue = value;
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        var value = 0f;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).intValue != value) continue;
                            value++;
                            if (value == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.floatValue = value;
                    }
                    break;
                case SerializedPropertyType.String:
                    {
                        prop.stringValue = string.Empty;
                    }
                    break;
                case SerializedPropertyType.Color:
                    {
                        var value = Color.black;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).colorValue != value) continue;
                            value = ToColor(ToInt(value) + 1);
                            if (value == Color.white)
                                break;
                            i = -1;
                        }
                        prop.colorValue = value;
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    {
                        prop.objectReferenceValue = null;
                    }
                    break;
                case SerializedPropertyType.LayerMask:
                    {
                        var value = -1;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).intValue != value) continue;
                            value++;
                            if (value == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.intValue = value;
                    }
                    break;
                case SerializedPropertyType.Enum:
                    {
                        var value = 0;
                        if (keysProp.arraySize > 1)
                        {
                            var first = keysProp.GetArrayElementAtIndex(0);
                            var max = first.enumNames.Length - 1;

                            for (int i = 0; i < keysProp.arraySize - 1; i++)
                            {
                                if (keysProp.GetArrayElementAtIndex(i).enumValueIndex != value) continue;
                                value++;
                                if (value >= max)
                                    break;
                                i = -1;
                            }
                        }
                        prop.enumValueIndex = value;
                    }
                    break;
                case SerializedPropertyType.Vector2:
                    {
                        var value = Vector2.zero;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).vector2Value != value) continue;
                            value.x++;
                            if (value.x == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.vector2Value = value;
                    }
                    break;
                case SerializedPropertyType.Vector3:
                    {
                        var value = Vector3.zero;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).vector3Value != value) continue;
                            value.x++;
                            if (value.x == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.vector3Value = value;
                    }
                    break;
                case SerializedPropertyType.Vector4:
                    {
                        var value = Vector4.zero;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).vector4Value != value) continue;
                            value.x++;
                            if (value.x == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.vector4Value = value;
                    }
                    break;
                case SerializedPropertyType.Rect:
                    {
                        prop.rectValue = Rect.zero;
                    }
                    break;
                case SerializedPropertyType.ArraySize:
                    {
                        var value = 0;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).arraySize != value) continue;
                            value++;
                            if (value == int.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.arraySize = value;
                    }
                    break;
                case SerializedPropertyType.Character:
                    {
                        var value = 0;
                        for (int i = 0; i < keysProp.arraySize - 1; i++)
                        {
                            if (keysProp.GetArrayElementAtIndex(i).intValue != value) continue;
                            value++;
                            if (value == char.MaxValue)
                                break;
                            i = -1;
                        }
                        prop.intValue = value;
                    }
                    break;
                case SerializedPropertyType.AnimationCurve:
                    {
                        prop.animationCurveValue = null;
                    }
                    break;
                case SerializedPropertyType.Bounds:
                    {
                        prop.boundsValue = default(Bounds);
                    }
                    break;
                default:
                    throw new System.InvalidOperationException("Can not handle Type as key.");
            }
        }

        private static int ToInt(Color color)
        {
            return (Mathf.RoundToInt(color.a * 255) << 24) +
                   (Mathf.RoundToInt(color.r * 255) << 16) +
                   (Mathf.RoundToInt(color.g * 255) << 8) +
                   Mathf.RoundToInt(color.b * 255);
        }

        private static Color ToColor(int value)
        {
            var a = (value >> 24 & 0xFF) / 255f;
            var r = (value >> 16 & 0xFF) / 255f;
            var g = (value >> 8 & 0xFF) / 255f;
            var b = (value & 0xFF) / 255f;
            return new Color(r, g, b, a);
        }

    }
}