using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditorInternal;

[CustomEditor(typeof(ExampleScript))]
public class ExampleScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExampleScript currentTarget = (ExampleScript)target;

        //Int Field
        currentTarget.myInteger = EditorGUILayout.IntField("Integer", currentTarget.myInteger);

        //Float Field
        currentTarget.myFloat = EditorGUILayout.FloatField("Float", currentTarget.myFloat);

        //Boolean Field
        currentTarget.myBool = EditorGUILayout.Toggle("Boolean", currentTarget.myBool);

        //String Field
        currentTarget.myString = EditorGUILayout.TextField("String", currentTarget.myString);

        //Color Field
        currentTarget.myColor = EditorGUILayout.ColorField("Color", currentTarget.myColor);

        //Gameobject
        currentTarget.myObject = (GameObject)EditorGUILayout.ObjectField("Gameobject", currentTarget.myObject, typeof(GameObject));

        //Image
        currentTarget.myImage = (Image)EditorGUILayout.ObjectField("Image", currentTarget.myImage, typeof(Image));

        //ScriptableObject
        currentTarget.myScriptableObject = (ExampleObject)EditorGUILayout.ObjectField("ScriptableObject", currentTarget.myScriptableObject, typeof(ExampleObject));

        //Script
        currentTarget.myScript = (ExampleScriptScript)EditorGUILayout.ObjectField("Script", currentTarget.myScript, typeof(ExampleScriptScript));

        //List

        //UnityEvent
        

        //Tag
        currentTarget.myTag = EditorGUILayout.TagField("Tag", currentTarget.myTag);

        //Layer
        currentTarget.myLayer = EditorGUILayout.LayerField("Layer", currentTarget.myLayer);

        //Button
        if (GUILayout.Button("Click Me"))
        {
            currentTarget.ButtonPress(currentTarget.myString);
        }

        //enum Options
        currentTarget.option = (OPTIONS)EditorGUILayout.EnumPopup("Options: ", currentTarget.option);

        //Foldout
        currentTarget.foldout = EditorGUILayout.Foldout(currentTarget.foldout, "foldout");
        if (currentTarget.foldout)
        {
            currentTarget.toggle = EditorGUILayout.Toggle("Toggle", currentTarget.toggle);
            if (currentTarget.toggle)
            {
                EditorGUILayout.HelpBox("Congratulations!", MessageType.None);
            }
        }

        //Helpbox
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);

        //Warning
        EditorGUILayout.HelpBox("This is a warning box", MessageType.Warning);

        //Error
        EditorGUILayout.HelpBox("This is an error box", MessageType.Error);

    }
}
