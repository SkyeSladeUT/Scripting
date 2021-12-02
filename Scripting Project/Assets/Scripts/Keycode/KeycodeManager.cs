using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void Initialize()
    {
        _currentString = "";
        PasswordText.text = "";
        passwordString = "";
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
            Debug.Log("Correct");
            //Flash Text Green Then Delete
        }
        else
        {
            Debug.Log("Incorrect");
            //Flash Text Red Then Delete
        }
    }


}
