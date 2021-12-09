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

    //Dialogue Variables
    private TextMeshProUGUI _characterText, _dialogueText;
    private GameObject _dialogueObject;
    private GameObject _continueArrow;

    //Choice Variables
    private GameObject _choiceObject;
    private GameObject _choiceLayoutParent;
    private GameObject _choicePrefab; //located in resources folder
    [SerializeField]
    private int movement;
    [SerializeField]
    private bool inChoice;
    private bool reset;

    private bool next;
    DialogueClasses.Line currentLine;
    private bool inConv;

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
            _characterObjects.Add(character.CharacterName, GameObject.Find(character.CharacterObjectName));
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
        _characterText = FindObject("Character Text").GetComponent<TextMeshProUGUI>();
        _dialogueText = FindObject("Dialogue Text").GetComponent<TextMeshProUGUI>();
        _dialogueObject = FindObject("Dialogue Setup").gameObject;
        _continueArrow = FindObject("Continue Arrow").gameObject;

        _choiceObject = FindObject("Choice Setup");
        _choiceLayoutParent = FindObject("Choice Layout");
        _choicePrefab = Resources.Load<GameObject>("Choice");
        movement = 0;
        inChoice = false;
        reset = true;
        inConv = false;
    }

    private GameObject FindObject(string findObjectName)
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach(var c in children)
        {
            if (c.gameObject.name == findObjectName)
            {
                return c.gameObject;
            }
        }
        return null;
    }

    private void Update()
    {
        if (inConv)
        {
            if (Input.GetButtonDown(InteractButton))
            {
                next = true;
            }
            if (inChoice)
            {
                if (Input.GetAxis("Vertical") > .25f)
                {
                    movement = 1;
                }
                else if (Input.GetAxis("Vertical") < -.25f)
                {
                    movement = -1;
                }
                else
                {
                    movement = 0;
                    reset = true;
                }
            }
        }
        else
            next = false;
    }

    public void StartConversation(string conversationName)
    {
        if (!inConv)
        {
            inConv = true;
            DialogueClasses.Conversation conversation = _dialogues[conversationName];
            StartCoroutine(RunConversation(conversation));
        }
    }

    private IEnumerator RunConversation(DialogueClasses.Conversation conversation)
    {
        if (conversation.Lines.Length > 0) {
            currentLine = conversation.Lines[0];
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
                    case DialogueClasses.LineTypes.Audio:
                        yield return StartCoroutine(RunAudio(currentLine));
                        break;

                }
                if(currentLine.NextLineName != "")
                    currentLine = conversation[currentLine.NextLineName];
                else
                    currentLine = null;
                yield return new WaitForFixedUpdate();
            }
            _dialogueObject.SetActive(false);
        }
        inConv = false;
    }

    private IEnumerator RunDialogue(DialogueClasses.Line line)
    {
        //Scroll Dialogue
        yield return new WaitForSeconds(.1f);
        next = false;
        string[] lines = line.DialogueText.Split('\n');
        _dialogueText.text = "";
        if(line.Thought)
            _dialogueText.color = _characters[line.CharacterName].ThoughtColor;
        else
            _dialogueText.color = _characters[line.CharacterName].CharacterColor;
        _characterText.text = line.CharacterName;
        _characterText.color = _characters[line.CharacterName].CharacterColor;
        _dialogueObject.SetActive(true);

        GameObject character;
        AudioSource source = null;
        if (line.CharacterAudio.Name != "")
        {
            if (_characterObjects.TryGetValue(line.CharacterName, out character))
            {
                source = character.GetComponentInChildren<AudioSource>();
                try
                {
                    AudioClip clip = Resources.Load<AudioClip>("Audio/" + line.CharacterAudio.Name);
                    source.clip = clip;
                    source.volume = line.CharacterAudio.Volume;
                    source.loop = line.CharacterAudio.Loop;
                }
                catch { }
            }
        }

        for(int i = 0; i < lines.Length; i++)
        {
            string s = lines[i].Trim();
            if(s.Length > 0 && s != " ")
                yield return StartCoroutine(RunLine(lines[i], source));
        }
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator RunLine(string line, AudioSource source)
    {
        int currentIndex = 0;
        string currentLine = "";
        if (source != null)
            source.Play();
        while(currentIndex < line.Length)
        {
            if (next)
            {
                next = false;
                _dialogueText.text = line;
                yield return new WaitForSeconds(.1f);
                break;
            }
            currentLine += line[currentIndex];
            _dialogueText.text = currentLine;
            yield return new WaitForSeconds(ScrollSpeed);
            currentIndex ++ ;
        }
        _continueArrow.SetActive(true);
        if (source != null)
            source.Stop();
        yield return new WaitUntil(()=>next);
        next = false;
        _continueArrow.SetActive(false);
    }

    private IEnumerator RunChoice(DialogueClasses.Line line)
    {
        //Give Choices
        List<TextMeshProUGUI> choiceTexts = new List<TextMeshProUGUI>();
        Color defaultColor = _characters[line.CharacterName].CharacterColor;
        Color choiceColor = _dialogues.ChoiceColor;
        Color chosenColor = defaultColor;
        chosenColor.a = .5f;
        for(int i = 0; i < line.Choices.Length; i++)
        {
            TextMeshProUGUI choiceText = Instantiate(_choicePrefab, _choiceLayoutParent.transform).GetComponent<TextMeshProUGUI>();
            choiceText.text = line.Choices[i].ChoiceText;
            if (line.Choices[i].chosen)
                choiceText.color = chosenColor;
            else
                choiceText.color = defaultColor;
            choiceTexts.Add(choiceText);
        }
        _dialogueObject.SetActive(false);
        _choiceObject.SetActive(true);
        int currentIndex = 0;
        choiceTexts[0].color = choiceColor;
        inChoice = true;
        next = false;
        while (!next)
        {
            if (reset)
            {
                if (movement == 1)
                {
                    reset = false;
                    if(line.Choices[currentIndex].chosen)
                        choiceTexts[currentIndex].color = chosenColor;
                    else
                        choiceTexts[currentIndex].color = defaultColor;
                    currentIndex--;
                    currentIndex = Mathf.Clamp(currentIndex, 0, line.Choices.Length - 1);
                    choiceTexts[currentIndex].color = choiceColor;
                }
                else if (movement == -1)
                {
                    reset = false;
                    if (line.Choices[currentIndex].chosen)
                        choiceTexts[currentIndex].color = chosenColor;
                    else
                        choiceTexts[currentIndex].color = defaultColor;
                    currentIndex++;
                    currentIndex = Mathf.Clamp(currentIndex, 0, line.Choices.Length - 1);
                    choiceTexts[currentIndex].color = choiceColor;
                }
            }
            yield return new WaitForFixedUpdate();
        }
        next = false;
        inChoice = false;
        line.Choices[currentIndex].chosen = true;
        line.NextLineName = line.Choices[currentIndex].LineName;
        _choiceObject.SetActive(false);
        foreach(var t in choiceTexts)
        {
            Destroy(t.gameObject);
        }
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
        yield return new WaitForSeconds(line.RunTime);
    }

    private IEnumerator RunAudio(DialogueClasses.Line line)
    {
        GameObject character;
        if (_characterObjects.TryGetValue(line.CharacterName, out character))
        {
            AudioSource source = character.GetComponentInChildren<AudioSource>();
            try
            {
                AudioClip clip = Resources.Load<AudioClip>("Audio/" + line.CharacterAudio.Name);
                source.clip = clip;
                source.volume = line.CharacterAudio.Volume;
                source.loop = line.CharacterAudio.Loop;
                source.Play();
            }
            catch { }
            yield return new WaitForSeconds(line.AudioTime);
            source.Stop();
        }
    }

}
