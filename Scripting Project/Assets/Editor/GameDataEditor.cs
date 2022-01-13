using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace GameManagement {
    [CustomPropertyDrawer(typeof(SColor))]
    public class SColorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var ColorRect = new Rect(position.x, position.y, position.width, 21);
            var r = property.FindPropertyRelative("r");
            var g = property.FindPropertyRelative("g");
            var b = property.FindPropertyRelative("b");
            var a = property.FindPropertyRelative("a");

            var color = new Color(r.floatValue, g.floatValue, b.floatValue, a.floatValue);
            color = EditorGUI.ColorField(ColorRect, "Highlight Color", color);
            r.floatValue = color.r;
            g.floatValue = color.g;
            b.floatValue = color.b;
            a.floatValue = color.a;

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
