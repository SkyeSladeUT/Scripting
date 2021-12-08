using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dialogue;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public string InteractButton;
    public float ScrollSpeed;

    private static DialogueController _instance;
    public static DialogueController Instance
    {
        get { return _instance; }
    }

    private DialogueContainer _dialogues;
    private CharacterList _characters;

    private Dictionary<string, Action> _dialogueFunctions;
    private Dictionary<string, GameObject> _characterObjects;

    private TextMeshProUGUI _characterText, _dialogueText;
    private GameObject _dialogueObject;
    private GameObject _choicePrefab; //located in resources folder
    private GameObject _continueArrow;

    private bool skip;

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
        _characterText = transform.Find("Character Text").GetComponent<TextMeshProUGUI>();
        _dialogueText = transform.Find("Dialogue Text").GetComponent<TextMeshProUGUI>();
        _dialogueObject = transform.Find("Dialogue Object").gameObject;
        _continueArrow = transform.Find("Continue Arrow").gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown(InteractButton))
        {
            skip = true;
        }
    }

    public void StartConversation(string conversationName)
    {
        DialogueClasses.Conversation conversation = _dialogues[conversationName];
        StartCoroutine(RunConversation(conversation));
    }

    private IEnumerator RunConversation(DialogueClasses.Conversation conversation)
    {
        if (conversation.Lines.Length > 0) {
            _dialogueObject.SetActive(true);
            DialogueClasses.Line currentLine = conversation.Lines[0];
            while (currentLine != null)
            {
                switch (currentLine.LineType)
                {
                    case DialogueClasses.LineTypes.Dialogue:
                        yield return StartCoroutine(RunDialogue(currentLine));
                        break;
                    case DialogueClasses.LineTypes.Animation:
                        yield return StartCoroutine(RunAnimation(currentLine));
                        break;
                    case DialogueClasses.LineTypes.Choice:
                        yield return StartCoroutine(RunChoice(currentLine));
                        break;
                    case DialogueClasses.LineTypes.Script:
                        yield return StartCoroutine(RunScript(currentLine));
                        break;
                }
                if(currentLine.NextLineName != "")
                {
                    currentLine = conversation[currentLine.NextLineName];
                }
                else
                {
                    currentLine = null;
                }
                yield return new WaitForFixedUpdate();
            }
            _dialogueObject.SetActive(false);
        }
    }

    private IEnumerator RunDialogue(DialogueClasses.Line line)
    {
        //Scroll Dialogue
        string[] lines = line.DialogueText.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            yield return StartCoroutine(RunLine(lines[i]));
        }
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunLine(string line)
    {
        int currentIndex = 0;
        while(currentIndex < line.Length)
        {
            if (skip)
            {
                _dialogueText.text = line;
                yield return new WaitForSeconds(.1f);
                break;
            }
            yield return new WaitForSeconds(ScrollSpeed);
        }
        _continueArrow.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown(InteractButton));
        _continueArrow.SetActive(false);
    }

    private IEnumerator RunChoice(DialogueClasses.Line line)
    {
        //Give Choices
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunAnimation(DialogueClasses.Line line)
    {
        GameObject character;
        if (_characterObjects.TryGetValue(line.CharacterName, out character))
        {
            try {
                Animator anim = character.GetComponentInChildren<Animator>();
                if (line.isInteger)
                {
                    anim.SetInteger(line.IntegerName, line.IntegerNum);
                }
                if (line.isBool)
                {
                    anim.SetBool(line.BoolName, line.BoolValue);
                }
                if (line.isTrigger)
                {
                    anim.SetTrigger(line.TriggerName);
                }
            } catch { Debug.LogError("Problem Trying to Run Animation"); }
        }
        yield return new WaitForSeconds(line.animationTime);
    }

    private IEnumerator RunScript(DialogueClasses.Line line)
    {
        Action lineAction;
        if(_dialogueFunctions.TryGetValue(line.LineName, out lineAction))
        {
            try
            {
                lineAction.Invoke();
            }
            catch { Debug.LogError("Problem Trying to Run Script"); }
        }
        yield return new WaitForFixedUpdate();
    }

}
