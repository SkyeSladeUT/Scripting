using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/Debug")]
public class DebugObject : ScriptableObject
{
    public void Call(string printStatement)
    {
        Debug.Log(printStatement);
    }
}
