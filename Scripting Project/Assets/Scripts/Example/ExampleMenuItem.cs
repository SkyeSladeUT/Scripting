using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExampleMenuItem
{
    [MenuItem("Tools/ExampleMenuItem #w")]
    private static void ExampleMenuOption()
    {
        Debug.Log("Menu Option Clicked");
    }
}
