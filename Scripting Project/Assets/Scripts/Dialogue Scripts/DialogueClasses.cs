using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
namespace GameManagement
{
    [Serializable]
    public class Conversation
    {
        public string ConversationName;
        public Line[] Lines;

        public Line this[string LineName]
        {
            get { return Lines.ToList().Find((n) => n.LineName == LineName); }
        }

        public Line this[int LineIndex]
        {
            get { return Lines[LineIndex]; }
        }
    }

    public enum LineTypes
    {
        Dialogue, Choice, Script, Animation, Audio
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Line))]
    public class LinePropertyDrawer : PropertyDrawer
    {
        float rectHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            rectHeight = 0;
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var LineNameRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
            rectHeight += 21;
            var CharacterNameRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
            rectHeight += 21;
            var LineTypeRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
            rectHeight += 21;

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

                    var ThoughtTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var thoughtBool = property.FindPropertyRelative("Thought");
                    thoughtBool.boolValue = EditorGUI.Toggle(ThoughtTextRect, "Is Thought", thoughtBool.boolValue);
                    var DialogueTextRect = new Rect(position.x, position.y + rectHeight, position.width, 105);
                    rectHeight += 105;
                    var dialogueText = property.FindPropertyRelative("DialogueText");
                    var style = new GUIStyle(EditorStyles.textField);
                    style.wordWrap = true;
                    style.fixedHeight = 100;
                    dialogueText.stringValue = EditorGUI.TextArea(DialogueTextRect, dialogueText.stringValue, style);

                    var ah = 21;
                    var characterAudio = property.FindPropertyRelative("CharacterAudio");
                    if (characterAudio.isExpanded)
                    {
                        ah += 60;
                    }
                    var CharacterAudioTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += ah;
                    EditorGUI.PropertyField(CharacterAudioTextRect, characterAudio, true);
                    break;
                #endregion
                #region ANIMATION CASE
                case LineTypes.Animation:
                    var animTime = property.FindPropertyRelative("animationTime");
                    var AnimTimeRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    animTime.floatValue = EditorGUI.FloatField(AnimTimeRect, "Animation Length", animTime.floatValue);
                    var isTrigger = property.FindPropertyRelative("isTrigger");
                    var IsTriggerTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    isTrigger.boolValue = EditorGUI.Toggle(IsTriggerTextRect, "Animation Trigger", isTrigger.boolValue);
                    if (isTrigger.boolValue)
                    {
                        //TriggerName
                        var triggerName = property.FindPropertyRelative("TriggerName");
                        var TriggerNameTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                        rectHeight += 21;
                        triggerName.stringValue = EditorGUI.TextField(TriggerNameTextRect, "Trigger Name", triggerName.stringValue);
                    }
                    var isInteger = property.FindPropertyRelative("isInteger");
                    var IsIntegerTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    isInteger.boolValue = EditorGUI.Toggle(IsIntegerTextRect, "Animation Integer", isInteger.boolValue);
                    if (isInteger.boolValue)
                    {
                        //IntegerName
                        var integerName = property.FindPropertyRelative("IntegerName");
                        var IntegerNameTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                        rectHeight += 21;
                        integerName.stringValue = EditorGUI.TextField(IntegerNameTextRect, "Integer Name", integerName.stringValue);
                        //IntegerNum
                        var integerNum = property.FindPropertyRelative("IntegerNum");
                        var IntergerNumTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                        rectHeight += 21;
                        integerNum.intValue = EditorGUI.IntField(IntergerNumTextRect, "Integer Num", integerNum.intValue);
                    }
                    var isBool = property.FindPropertyRelative("isBool");
                    var IsBoolTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    isBool.boolValue = EditorGUI.Toggle(IsBoolTextRect, "Animation Bool", isBool.boolValue);
                    if (isBool.boolValue)
                    {
                        //BoolName
                        var boolName = property.FindPropertyRelative("BoolName");
                        var BoolNameTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                        rectHeight += 21;
                        boolName.stringValue = EditorGUI.TextField(BoolNameTextRect, "Bool Name", boolName.stringValue);
                        //BoolValue
                        var boolValue = property.FindPropertyRelative("BoolValue");
                        var BoolValueTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                        rectHeight += 21;
                        boolValue.boolValue = EditorGUI.Toggle(BoolValueTextRect, "Bool Value", boolValue.boolValue);
                    }
                    break;
                #endregion
                #region CHOICE CASE
                case LineTypes.Choice:

                    var choicesList = property.FindPropertyRelative("Choices");
                    var height = 25;
                    if (choicesList.isExpanded)
                    {
                        height += 50;
                        for (int i = 0; i < choicesList.arraySize; i++)
                        {
                            if (choicesList.GetArrayElementAtIndex(i).isExpanded)
                            {
                                height += 80;
                            }
                            else
                            {
                                height += 25;
                            }
                        }
                    }
                    var ChoicesRect = new Rect(position.x, position.y + rectHeight, position.width, height);
                    EditorGUI.PropertyField(ChoicesRect, choicesList, true);
                    rectHeight += height;

                    break;
                #endregion
                #region SCRIPT CASE
                case LineTypes.Script:
                    var GameObjectTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var gameObjectName = property.FindPropertyRelative("GameObjectName");
                    gameObjectName.stringValue = EditorGUI.TextField(GameObjectTextRect, "Game Object Name", gameObjectName.stringValue);
                    var ScriptTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var scriptName = property.FindPropertyRelative("ScriptName");
                    scriptName.stringValue = EditorGUI.TextField(ScriptTextRect, "Script Name", scriptName.stringValue);
                    var FunctionTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var functionName = property.FindPropertyRelative("FunctionName");
                    functionName.stringValue = EditorGUI.TextField(FunctionTextRect, "Function Name", functionName.stringValue);
                    var RunTimeTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var runTime = property.FindPropertyRelative("RunTime");
                    runTime.floatValue = EditorGUI.FloatField(RunTimeTextRect, "Run Time", runTime.floatValue);
                    break;
                #endregion
                #region AUDIO CASE
                case LineTypes.Audio:
                    var AudioTimeTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += 21;
                    var audioTime = property.FindPropertyRelative("AudioTime");
                    audioTime.floatValue = EditorGUI.FloatField(AudioTimeTextRect, "Audio Time", audioTime.floatValue);
                    var aa = 21;
                    var audio = property.FindPropertyRelative("CharacterAudio");
                    if (audio.isExpanded)
                    {
                        aa += 60;
                    }
                    var AudioTextRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
                    rectHeight += aa;
                    EditorGUI.PropertyField(AudioTextRect, audio, true);
                    break;
                    #endregion
            }

            var NextLineRect = new Rect(position.x, position.y + rectHeight, position.width, 21);
            var nextLine = property.FindPropertyRelative("NextLineName");
            nextLine.stringValue = EditorGUI.TextField(NextLineRect, "Next Line", nextLine.stringValue);

            rectHeight += 42;
            property.FindPropertyRelative("height").floatValue = rectHeight;
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();

        }

        //This will need to be adjusted based on what you are displaying
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + Mathf.Clamp(property.FindPropertyRelative("height").floatValue, 21, 99999999);
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
        public Audio CharacterAudio;
        public bool Thought;
        [TextArea(5, 5)]
        public string DialogueText;
        #endregion
        #region ANIMATION VARIABLES
        public float animationTime;
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
        public float RunTime;
        #endregion
        #region AUDIO VARIABLES
        public float AudioTime;
        #endregion
        public float height;
        public string NextLineName;
    }

    [Serializable]
    public class Choice
    {
        public string LineName;
        public string ChoiceText;
        public bool chosen;
    }

    [Serializable]
    public class Audio
    {
        public string Name;
        public float Volume;
        public bool Loop;
    }
}
