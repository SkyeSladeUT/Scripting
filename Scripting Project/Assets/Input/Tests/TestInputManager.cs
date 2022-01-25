using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputManager : MonoBehaviour
{
    InputMaster _input;
    TestAnimatorManager _animatorManager;
    TestPlayerLocomotion _playerlocomotion;
    private Vector2 _movementInput, _cameraInput;
    [HideInInspector]
    public float verticalInput, horizontalInput, camInputX, camInputY, moveAmount;
    [HideInInspector]
    public bool Sprinting, JumpInput;

    private void Awake()
    {
        _animatorManager = GetComponent<TestAnimatorManager>();
        _playerlocomotion = GetComponent<TestPlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new InputMaster();
            _input.Player.Walk.performed += context => _movementInput = context.ReadValue<Vector2>();
            _input.Player.CameraRotate.performed += context => _cameraInput = context.ReadValue<Vector2>();

            _input.Player.Sprint.performed += context => Sprinting = true;
            _input.Player.Sprint.canceled += context => Sprinting = false;

            _input.Player.Jump.performed += context => JumpInput = true;
        }

        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        verticalInput = _movementInput.y;
        horizontalInput = _movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, moveAmount, Sprinting);

        camInputX = _cameraInput.x;
        camInputY = _cameraInput.y;
    }

    private void HandleJump()
    {
        if (JumpInput)
        {
            JumpInput = false;
            _playerlocomotion.HandleJump();
        }
    }

}
