using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CharacterInputManager _characterInput;

    public void Initialize()
    {
        if(_characterInput == null)
        {
            _characterInput = new CharacterInputManager(this.gameObject, Camera.main.transform);
            EnableCharacter();
        }
    }

    public void EnableCharacter()
    {
        _characterInput.Enable();
        HideMouse();
    }

    public void DisableCharacter()
    {
        _characterInput.Disable();
    }

    private void Update()
    {
        if(_characterInput != null)
        {
            _characterInput.OnUpdate();
        }
    }

    public void HideMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
