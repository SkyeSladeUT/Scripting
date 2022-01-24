using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager
{
    private InputMaster _input;
    public InputMaster Input { get { return _input; } }

    private CharacterController _controller;
    public CharacterController Controller
    {
        get
        {
            if (_controller == null)
                _controller = Character.GetComponent<CharacterController>();
            return _controller;
        }
    }

    private GameObject _character;
    public GameObject Character
    {
        get { return _character; }
    }

    private float _speed = .5f;
    private float _runMultiplier = 1.25f;
    private float _turnSmoothTime = .1f;
    private float _turnSmoothVelocity;

    private Transform _cam;
    public CharacterInputManager(GameObject character, Transform cam)
    {
        _character = character;
        _cam = cam;
        Initialize();
    }

    private void Initialize() 
    { 
        _input = new InputMaster();
        Input.Player.Jump.performed += (context) => OnJump();
        Input.Player.Interact.performed += (context) => OnInteract();
    }
    public void Enable() { Input.Enable(); }
    public void Disable() { Input.Disable(); }
    public void OnUpdate()
    {
        if (Input.Player.Walk.phase == InputActionPhase.Started)
        {
            Vector2 inputDirection = Input.Player.Walk.ReadValue<Vector2>();
            Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;
            if (direction.magnitude >= .1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(Character.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                Character.transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                moveDirection = moveDirection.normalized;
                moveDirection *= _speed * Time.deltaTime;
                if (Input.Player.Run.ReadValue<float>() > 0)
                    moveDirection *= _runMultiplier;

                Controller.Move(moveDirection);
            }
        }
    }
    public void OnJump()
    {
    }
    public void OnInteract()
    {
    }
}
