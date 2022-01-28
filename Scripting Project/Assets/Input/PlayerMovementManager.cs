using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    #region FALLING VARIABLES
    [Header("Falling")]
    public LayerMask GroundLayer;

    public float ForwardFallVelocity = 3;
    public float FallingVelocity = 33;

    public float RaycastHeightOffset = .1f;
    public float RaycastRadius = .1f;
    public float RaycastMaxDistance = .5f;
    public float FloatingColliderOffset = 0;

    private float _airTimer;
    #endregion

    #region MOVEMENT VARIABLES
    [Header("Movement Speeds")]
    public float WalkingSpeed = 1;
    public float RunningSpeed = 2;
    public float SprintingSpeed = 5;
    public float RotationSpeed = 50;
    #endregion

    #region JUMP VARIABLES
    [Header("Jump")]
    public float GravityIntensity = -15;
    public float JumpHeight = 3;
    #endregion

    [SerializeField]
    private bool _isGrounded;

    private Vector3 _moveDirection;
    private Transform _cameraObject;
    private Rigidbody _rigidbody;

    private PlayerAnimationManager _playerAnimation;
    public PlayerAnimationManager PlayerAnimation
    {
        get
        {
            if (_playerAnimation == null)
                _playerAnimation = new PlayerAnimationManager(GetComponentInChildren<Animator>());
            return _playerAnimation;
        }
    }

    private void Awake()
    {
        _cameraObject = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            _rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (PlayerAnimation.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
        PlayerAnimation.UpdateMovementValues(GetMoveAmount());
    }

    public void HandleMovement()
    {
        if (PlayerAnimation.isJumping)
            return;
        _moveDirection = _cameraObject.forward * InputVariables.VerticalInput;
        _moveDirection += (_cameraObject.right * InputVariables.HorizontalInput);
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        float moveAmount = GetMoveAmount();
        if(InputVariables.Sprinting && moveAmount > .5f)
            _moveDirection *= SprintingSpeed;
        else
        {
            if (moveAmount >= .5f)
                _moveDirection *= RunningSpeed;
            else
                _moveDirection *= WalkingSpeed;
        }
        Vector3 movementVelocity = _moveDirection;
        _rigidbody.velocity = movementVelocity;
    }

    public void HandleRotation()
    {
        if (PlayerAnimation.isJumping)
            return;
        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraObject.forward * InputVariables.VerticalInput;
        targetDirection += (_cameraObject.right * InputVariables.HorizontalInput);
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        rayCastOrigin.y += RaycastHeightOffset;
        if(!_isGrounded && !PlayerAnimation.isJumping)
        {
            if (!PlayerAnimation.isInteracting)
            {
                PlayerAnimation.PlayTargetAnimation("Falling", true);
            }
            _airTimer += Time.deltaTime;
            _rigidbody.AddForce(transform.forward * ForwardFallVelocity);
            _rigidbody.AddForce(-Vector3.up * FallingVelocity * _airTimer);
        }
        if (Physics.SphereCast(rayCastOrigin, RaycastRadius, -Vector3.up, out hit, RaycastMaxDistance, GroundLayer))
        {
            if (!_isGrounded && !_playerAnimation.isInteracting)
            {
                PlayerAnimation.PlayTargetAnimation("Landing", true);
            }
            _airTimer = 0;
            _isGrounded = true;
            PlayerAnimation.isGrounded = _isGrounded;
        }
        else
        {
            _isGrounded = false;
            PlayerAnimation.isGrounded = _isGrounded;
        }
    }

    public void HandleJumping()
    {
        if (_isGrounded)
        {
            PlayerAnimation.isJumping = true;
            PlayerAnimation.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * GravityIntensity * JumpHeight);
            Vector3 playerVelocity = _moveDirection;
            playerVelocity.y = jumpingVelocity;
            _rigidbody.velocity = playerVelocity;
        }
    }


    private float GetMoveAmount()
    {
        return Mathf.Clamp01(Mathf.Abs(InputVariables.HorizontalInput) + Mathf.Abs(InputVariables.VerticalInput));
    }
}
