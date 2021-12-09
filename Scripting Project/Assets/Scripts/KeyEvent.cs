using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEvent : MonoBehaviour
{
    public UnityEvent OnKey;
    public KeyCode Key;

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            OnKey.Invoke();
        }
    }
}
