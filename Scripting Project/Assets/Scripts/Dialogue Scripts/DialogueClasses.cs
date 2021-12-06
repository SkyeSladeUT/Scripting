using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace Dialogue
{
    public class DialogueClasses : MonoBehaviour
    {
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

                var LineNameRect = new Rect(position.x, position.y + (20 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                numLines++;
                var CharacterNameRect = new Rect(position.x, position.y + (20 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                numLines++;
                var LineTypeRect = new Rect(position.x, position.y + (20 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                numLines++;

                var lineName = property.FindPropertyRelative("LineName");
                var characterName = property.FindPropertyRelative("CharacterName");
                var lineType = property.FindPropertyRelative("LineType");

                lineName.stringValue = EditorGUI.TextField(LineNameRect, "Line Name", lineName.stringValue);
                characterName.stringValue = EditorGUI.TextField(CharacterNameRect, "Character Name", characterName.stringValue);
                lineType.intValue = EditorGUI.Popup(LineTypeRect, "Line Type", lineType.intValue, lineType.enumNames);

                switch ((LineTypes)lineType.intValue)
                {
                    #region DIALOGUE CASE
                    case LineTypes.Dialogue:
                        var DialogueTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines+=5;
                        var dialogueText = property.FindPropertyRelative("DialogueText");
                        var style = new GUIStyle(EditorStyles.textField);
                        style.wordWrap = true;
                        style.fixedHeight = 100;
                        dialogueText.stringValue = EditorGUI.TextArea(DialogueTextRect, "Dialogue Text", style);
                        break;
                    #endregion
                    #region ANIMATION CASE
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
                    #endregion
                    #region CHOICE CASE
                    case LineTypes.Choice:
                        var ChoicesRect = new Rect(position.x, position.y + (20 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        var choicesList = property.FindPropertyRelative("Choices");
                        EditorGUI.PropertyField(ChoicesRect, choicesList, true);
                        numLines += 1;
                        if (choicesList.isExpanded)
                        {
                            for (int i = 0; i < choicesList.arraySize; i++)
                            {
                                if (choicesList.GetArrayElementAtIndex(i).isExpanded)
                                {
                                    numLines += 2;
                                }
                                else
                                {
                                    numLines += 1;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region SCRIPT CASE
                    case LineTypes.Script:
                        var GameObjectTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        var gameObjectName = property.FindPropertyRelative("GameObjectName");
                        gameObjectName.stringValue = EditorGUI.TextField(GameObjectTextRect, "Game Object Name", gameObjectName.stringValue);
                        var ScriptTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        var scriptName = property.FindPropertyRelative("GameObjectName");
                        scriptName.stringValue = EditorGUI.TextField(ScriptTextRect, "Script Name", scriptName.stringValue);
                        var FunctionTextRect = new Rect(position.x, position.y + (21 * numLines), position.width, EditorGUIUtility.singleLineHeight);
                        numLines++;
                        var functionName = property.FindPropertyRelative("GameObjectName");
                        functionName.stringValue = EditorGUI.TextField(FunctionTextRect, "Function Name", functionName.stringValue);
                        break;
                        #endregion
                }
                numLines++;
                EditorGUI.indentLevel = indent;

                EditorGUI.EndProperty();
            }

            //This will need to be adjusted based on what you are displaying
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return (20 - EditorGUIUtility.singleLineHeight) + (21 * Mathf.Clamp(numLines, 1, 9999));
            }
        }
#endif

        [Serializable]
        public class Line
        {
            public string LineName;
            public string CharacterName;
            public LineTypes LineType;
            #region DIALOGUE VARIABLES
            [TextArea(5,5)]
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
            #region CHOICE VARIABLES
            public Choice[] Choices;
            #endregion
            #region SCRIPT VARIABLES
            public string GameObjectName;
            public string ScriptName;
            public string FunctionName;
            #endregion
        }

        [Serializable]
        public class Choice
        {
            public string LineName;
        }
    }
}
