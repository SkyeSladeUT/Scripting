using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DigitalRuby.Tween;

public class KeycodeManager : MonoBehaviour
{
    public string CorrectString;
    private string _currentString;
    public string CurrentString
    {
        get { return CurrentString; }
    }

    public int MaxLength = 6;
    public TextMeshProUGUI PasswordText;
    private string passwordString;
    public Color CorrectColor, IncorrectColor;
    private Color initColor, clearColor;
    public AudioSource audioSource;
    public AudioClip successClip, failClip;

    private void Awake()
    {
        if (PasswordText == null) PasswordText = GetComponentInChildren<TextMeshProUGUI>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void Initialize()
    {
        _currentString = "";
        PasswordText.text = "";
        passwordString = "";
        initColor = PasswordText.color;
        clearColor = initColor;
        clearColor.a = 0;
    }

    public void EnterNum(string num)
    {
        if (_currentString.Length < MaxLength)
        {
            _currentString += num;
            passwordString += "*";
            PasswordText.text = passwordString;
        }
    }

    public void RemoveNum()
    {
        if(_currentString.Length > 0)
        {
            _currentString = _currentString.Substring(0, _currentString.Length - 1);
            passwordString = passwordString.Substring(0, _currentString.Length - 1);
            PasswordText.text = passwordString;
        }
    }

    public void ClearNum()
    {
        _currentString = "";
        passwordString = "";
        PasswordText.text = "";
    }

    public void Submit()
    {
        if(_currentString == CorrectString)
        {
            //Flash Text Green Then Delete
            audioSource.clip = successClip;
            audioSource.Play();
            clearColor.a = 0;
            gameObject.Tween(gameObject.name + "Correct", initColor, CorrectColor, .1f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; })
                .ContinueWith(new ColorTween().Setup(CorrectColor, clearColor, .2f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; }))
                .ContinueWith(new ColorTween().Setup(clearColor, CorrectColor, .1f, TweenScaleFunctions.CubicEaseInOut, (t)=> { PasswordText.color = t.CurrentValue; }))
                .ContinueWith(new ColorTween().Setup(CorrectColor, clearColor, .2f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; }, 
                (t)=> {
                    ClearNum();
                    PasswordText.color = initColor;
                }));
        }
        else
        {
            //Flash Text Red Then Delete
            audioSource.clip = failClip;
            audioSource.Play();
            clearColor.a = 0;
            gameObject.Tween(gameObject.name + "Incorrect", initColor, IncorrectColor, .1f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; })
                .ContinueWith(new ColorTween().Setup(IncorrectColor, clearColor, .2f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; }))
                .ContinueWith(new ColorTween().Setup(clearColor, IncorrectColor, .1f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; }))
                .ContinueWith(new ColorTween().Setup(IncorrectColor, clearColor, .2f, TweenScaleFunctions.CubicEaseInOut, (t) => { PasswordText.color = t.CurrentValue; },
                (t) => {
                    ClearNum();
                    PasswordText.color = initColor;
                }));
        }
    }


}
