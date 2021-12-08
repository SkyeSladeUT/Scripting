using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Dialogue;

public class DialogueController : MonoBehaviour
{
    private static DialogueController _instance;
    public static DialogueController Instance
    {
        get { return _instance; }
    }

    private DialogueContainer _dialogues;
    private CharacterList _characters;

    private Dictionary<string, Action> _dialogueFunctions;
    private Dictionary<string, GameObject> _characterObjects;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        _characters = Resources.Load<CharacterList>("Characters");
        _characterObjects = new Dictionary<string, GameObject>();
        foreach(var character in _characters.characters)
        {
            _characterObjects.Add(character.CharacterName, GameObject.Find(character.CharacterPrefab.name));
        }
        _dialogues = Resources.Load<DialogueContainer>("Dialogues");
        _dialogueFunctions = new Dictionary<string, Action>();
        foreach(var conv in _dialogues.conversations)
        {
            foreach(var line in conv.Lines)
            {
                if(line.LineType == DialogueClasses.LineTypes.Script)
                {
                    try
                    {
                        GameObject scriptObj = GameObject.Find(line.GameObjectName);
                        var script = scriptObj.GetComponent(line.ScriptName);
                        _dialogueFunctions.Add(line.LineName, () => { script.SendMessage(line.FunctionName); });
                    }
                    catch { Debug.Log("Script Object Could Not Be Found"); }
                }
            }
        }
    }

    public void StartConversation(string conversationName)
    {
        DialogueClasses.Conversation conversation = _dialogues[conversationName];
    }

    private IEnumerator RunConversation(DialogueClasses.Conversation conversation)
    {
        if (conversation.Lines.Length > 0) {
            DialogueClasses.Line currentLine = conversation.Lines[0];
            while (currentLine != null)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private IEnumerator RunDialogue(DialogueClasses.Line line)
    {
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunChoice(DialogueClasses.Line line)
    {
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunAnimation(DialogueClasses.Line line)
    {
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunScript(DialogueClasses.Line line)
    {
        yield return new WaitForFixedUpdate();
    }



}
