using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTests : MonoBehaviour
{
    private InputTests _instance;
    public InputTests Instance
    {
        get { return _instance; }
    }
    private InputMaster _input;
    public InputMaster Input
    {
        get 
        {
            if (_input == null)
                _input = new InputMaster();
            return _input; 
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Input.Player.Jump.performed += (context) => OnJump();
        Input.Player.Walk.performed += (context) => OnMove(context.ReadValue<Vector2>());
    }

    /*private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame)
        {

        }
    }*/

    private void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }

    private void OnJump()
    {
        Debug.Log("Jumped");
    }

    private void OnMove(Vector2 direction)
    {
        Debug.Log("Moved: " + direction);
    }
}
