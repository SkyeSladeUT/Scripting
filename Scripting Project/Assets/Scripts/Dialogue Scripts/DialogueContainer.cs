using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public int MaxCharactersPerLine;
        public float scrollSpeed;
        public List<Conversation> conversations;
    }
    [Serializable]
    public class Conversation
    {
        public string ConversationName;
        public List<Line> lines;
    }

    public enum LineTypes
    {
        Dialogue, Choice, Script, Animation
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Line))]
    public class LinePropertyDrawer : PropertyDrawer
    {
        int numLines = 0;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            numLines = 0;
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var CharacterNameRect = new Rect(position.x, position.y + (20*numLines), position.width, EditorGUIUtility.singleLineHeight);
            numLines++;
            var LineTypeRect = new Rect(position.x, position.y + (20*numLines), position.width, EditorGUIUtility.singleLineHeight);
            numLines++;

            var characterName = property.FindPropertyRelative("CharacterName");
            var lineType = property.FindPropertyRelative("LineType");

            characterName.stringValue = EditorGUI.TextField(CharacterNameRect, "Character Name", characterName.stringValue);
            lineType.intValue = EditorGUI.Popup(LineTypeRect, "Line Type", lineType.intValue, lineType.enumNames);

            switch ((LineTypes)lineType.intValue)
            {
                case LineTypes.Dialogue:
                    var DialogueTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                    numLines++;
                    var dialogueText = property.FindPropertyRelative("DialogueText");
                    dialogueText.stringValue = EditorGUI.TextField(DialogueTextRect, "Dialogue Text", dialogueText.stringValue);
                    break;
                case LineTypes.Animation:
                    var isTrigger = property.FindPropertyRelative("isTrigger");
                    var IsTriggerTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                    numLines++;
                    isTrigger.boolValue = EditorGUI.Toggle(IsTriggerTextRect, "Animation Trigger", isTrigger.boolValue);
                    if (isTrigger.boolValue)
                    {
                        //TriggerName
                        var triggerName = property.FindPropertyRelative("TriggerName");
                        var TriggerNameTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        triggerName.stringValue = EditorGUI.TextField(TriggerNameTextRect, "Trigger Name", triggerName.stringValue);
                    }
                    var isInteger = property.FindPropertyRelative("isInteger");
                    var IsIntegerTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                    numLines++;
                    isInteger.boolValue = EditorGUI.Toggle(IsIntegerTextRect, "Animation Integer", isInteger.boolValue);
                    if (isInteger.boolValue)
                    {
                        //IntegerName
                        var integerName = property.FindPropertyRelative("IntegerName");
                        var IntegerNameTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        integerName.stringValue = EditorGUI.TextField(IntegerNameTextRect, "Integer Name", integerName.stringValue);
                        //IntegerNum
                        var integerNum = property.FindPropertyRelative("IntegerNum");
                        var IntergerNumTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        integerNum.intValue = EditorGUI.IntField(IntergerNumTextRect, "Integer Num", integerNum.intValue);
                    }
                    var isBool = property.FindPropertyRelative("isBool");
                    var IsBoolTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                    numLines++;
                    isBool.boolValue = EditorGUI.Toggle(IsBoolTextRect, "Animation Bool", isBool.boolValue);
                    if (isBool.boolValue)
                    {
                        //BoolName
                        var boolName = property.FindPropertyRelative("BoolName");
                        var BoolNameTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        boolName.stringValue = EditorGUI.TextField(BoolNameTextRect, "Bool Name", boolName.stringValue);
                        //BoolValue
                        var boolValue = property.FindPropertyRelative("BoolValue");
                        var BoolValueTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        boolValue.boolValue = EditorGUI.Toggle(BoolValueTextRect, "Bool Value", boolValue.boolValue);
                    }
                    break;
                case LineTypes.Choice:
                    break;
                case LineTypes.Script:
                    break;
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        //This will need to be adjusted based on what you are displaying
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + (21 * Mathf.Clamp(numLines, 1, 9999));
        }

        /*public override void OnInspectorGUI()
        {
            Line _script = (Line)target;
            _script.LineType = (LineTypes)EditorGUILayout.EnumPopup("Line Type: ", _script.LineType);
            switch (_script.LineType)
            {
                case LineTypes.Dialogue:
                    _script.Text = EditorGUILayout.TextArea("Dialogue Text: ", _script.Text);
                    break;
                case LineTypes.Animation:
                    _script.isTrigger = EditorGUILayout.Toggle("Trigger", _script.isTrigger);
                    if (_script.isTrigger)
                    {
                        _script.TriggerName = EditorGUILayout.TextField("Trigger Name: ", _script.TriggerName);
                    }
                    _script.isInteger = EditorGUILayout.Toggle("Integer", _script.isInteger);
                    if (_script.isInteger)
                    {
                        _script.IntegerName = EditorGUILayout.TextField("Integer Name: ", _script.IntegerName);
                        _script.IntegerNum = EditorGUILayout.IntField("Integer Value: ", _script.IntegerNum);
                    }
                    _script.isBool = EditorGUILayout.Toggle("Bool", _script.isBool);
                    if (_script.isBool)
                    {
                        _script.BoolName = EditorGUILayout.TextField("Bool Name: ", _script.BoolName);
                        _script.BoolValue = EditorGUILayout.Toggle("Bool Value: ", _script.BoolValue);
                    }
                    break;
                case LineTypes.Choice:

                    break;
                case LineTypes.Script:
                    _script.GameObjectName = EditorGUILayout.TextField("Game Object Name: ", _script.GameObjectName);
                    _script.ScriptName = EditorGUILayout.TextField("Script Name: ", _script.ScriptName);
                    _script.FunctionName = EditorGUILayout.TextField("Function Name: ", _script.FunctionName);
                    break;
            }
        }*/
    }
#endif
    [Serializable]
    public class TestClass
    {

    }

    [Serializable]
    public class Line
    {
        public string CharacterName;
        public LineTypes LineType;
        #region DIALOGUE VARIABLES
        [TextArea]
        public string DialogueText;
        #endregion
        #region ANIMATION VARIABLES
        public bool isTrigger;
        public string TriggerName;
        public bool isInteger;
        public string IntegerName;
        public int IntegerNum;
        public bool isBool;
        public string BoolName;
        public bool BoolValue;
        #endregion
       /* #region CHOICE VARIABLES
        public List<Choice> choices;
        #endregion
        #region SCRIPT VARIABLES
        public string GameObjectName;
        public string ScriptName;
        public string FunctionName;
        #endregion*/
    }

    [Serializable]
    public class Choice
    {
        public string ChoiceString;
        public Line ChoiceAction;
    }
}

