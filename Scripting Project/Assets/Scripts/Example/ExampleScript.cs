using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum OPTIONS
{
    OPTION1 = 0,
    OPTION2 = 1,
    OPTION3 = 2
}

public class ExampleScript : MonoBehaviour
{
    public int myInteger;
    public float myFloat;
    public bool myBool;
    public string myString;
    public Color myColor;

    public GameObject myObject;
    public Image myImage;

    public string myTag;
    public int myLayer;

    public ExampleObject myScriptableObject;
    public ExampleScriptScript myScript;

    public List<String> myList;
    public UnityEvent unityEvent;

    public OPTIONS option;

    [HideInInspector]
    public bool foldout, toggle;

    public void ButtonPress(string debugstring)
    {
        if (debugstring == "")
        {
            Debug.Log("You Pressed A Button");
        }
        else
        {
            Debug.Log(debugstring);
        }
    }

}
