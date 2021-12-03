using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DigitalRuby.Tween;
public enum KeyType
{
    number, delete, clear, submit
}

public class KeyTap : MonoBehaviour
{
    public KeyType keyType;
    public string key;
    private KeycodeManager manager;
    private AudioSource audioSource;

    [CustomEditor(typeof(KeyTap))]
    public class KeyTapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            KeyTap _script = (KeyTap)target;
            _script.keyType = (KeyType)EditorGUILayout.EnumPopup("Key Type: ", _script.keyType);
            if(_script.keyType == KeyType.number)
            {
                _script.key = EditorGUILayout.TextField("Key", _script.key);
            }
        }
    }


    private void Awake()
    {
        manager = gameObject.GetComponentInParent<KeycodeManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        Press();
        switch (keyType)
        {
            case KeyType.number:
                manager.EnterNum(key);
                break;
            case KeyType.delete:
                manager.RemoveNum();
                break;
            case KeyType.clear:
                manager.ClearNum();
                break;
            case KeyType.submit:
                manager.Submit();
                break;
        }
    }

    private void Press()
    {
        audioSource.Play();
        Vector3 initPosition = transform.localPosition;
        Vector3 pressedPosition = transform.localPosition;
        pressedPosition.z -= .0125f;
        gameObject.Tween(gameObject.name + "Press", initPosition, pressedPosition, .1f, TweenScaleFunctions.CubicEaseIn, (t) => { transform.localPosition = t.CurrentValue; })
            .ContinueWith(new Vector3Tween().Setup(pressedPosition, initPosition, .25f, TweenScaleFunctions.CubicEaseOut, (t) => { transform.localPosition = t.CurrentValue; }));
    }
}
