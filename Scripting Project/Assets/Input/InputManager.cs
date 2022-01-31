using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputManager _instance;
    public InputManager Instance
    {
        get { return _instance; }
    }

    private InputMaster _inputMaster;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        if(_inputMaster == null)
        {
            _inputMaster = new InputMaster();
            _inputMaster.Player.Walk.performed += context =>
            {
                Vector2 value = context.ReadValue<Vector2>();
                InputVariables.HorizontalInput = value.x;
                InputVariables.VerticalInput = value.y;
            };
            _inputMaster.Player.CameraRotate.performed += context =>
            {
                Vector2 value = context.ReadValue<Vector2>();
                InputVariables.HorizontalCamera = value.x;
                InputVariables.VerticalCamera = value.y;
            };

            _inputMaster.Player.Sprint.performed += context => InputVariables.Sprinting = true;
            _inputMaster.Player.Sprint.canceled += context => InputVariables.Sprinting = false;

            _inputMaster.Player.Jump.performed += context => InputVariables.Jump = true;

            _inputMaster.Player.Crawl.performed += context => InputVariables.Crouching = true;
            _inputMaster.Player.Crawl.canceled += context => InputVariables.Crouching = false;
        }
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}

public static class InputVariables 
{
    public static float VerticalInput;
    public static float HorizontalInput;
    public static float VerticalCamera;
    public static float HorizontalCamera;
    public static bool Sprinting;
    public static bool Jump;
    public static bool Crouching;
}
