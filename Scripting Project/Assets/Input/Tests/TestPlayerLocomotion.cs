using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerLocomotion : MonoBehaviour
{
    TestInputManager _manager;
    TestPlayerManager _playerManager;
    TestAnimatorManager _animatorManager;
    Vector3 _moveDirection;
    Transform _cameraObject;
    Rigidbody _rigidbody;

    [Header("Falling")]
    private float inAirTimer;
    public float LeapingVelocity = 3;
    public float FallingVelocity = 33;
    public LayerMask GroundLayer;
    public float rayCastHeightOffset = .01f;
    public float RaycastRadius = .01f;
    public float RaycastMaxDistance = .05f;
    public float FloatingColliderOffset;

    [Header("Movement Flags")]
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float WalkingSpeed = 1f;
    public float RunningSpeed = 2;
    public float SprintingSpeed = 5;
    public float RotationSpeed = 50;

    [Header("Jump")]
    public float gravityIntensity = -15;
    public float jumpHeight = 3;

    private void Awake()
    {
        _manager = GetComponent<TestInputManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
        _playerManager = GetComponent<TestPlayerManager>();
        _animatorManager = GetComponent<TestAnimatorManager>();
    }
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (_playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        if (isJumping)
            return;
        _moveDirection = _cameraObject.forward * _manager.verticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _manager.horizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        if (_manager.Sprinting && _manager.moveAmount > .5f)
        {
            _moveDirection = _moveDirection * SprintingSpeed;
        }
        else
        {
            if (_manager.moveAmount >= .5f)
            {
                _moveDirection *= RunningSpeed;
            }
            else
            {
                _moveDirection *= WalkingSpeed;
            }
        }

        Vector3 movementVelocity = _moveDirection;

        _rigidbody.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        if (isJumping)
            return;
        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraObject.forward * _manager.verticalInput;
        targetDirection = targetDirection + _cameraObject.right * _manager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;
        if (!isGrounded && !isJumping)
        {
            if (!_playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("EmptyFalling", true);
            }

            inAirTimer += Time.deltaTime;
            _rigidbody.AddForce(transform.forward * LeapingVelocity);
            _rigidbody.AddForce(-Vector3.up * FallingVelocity * inAirTimer);
        }
        if (Physics.SphereCast(rayCastOrigin, RaycastRadius, -Vector3.up, out hit, RaycastMaxDistance, GroundLayer))
        {
            if (!isGrounded && !_playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("EmptyLanding", true);
            }

            //Vector3 raycastHitPoint = hit.point;
            //targetPosition.y = raycastHitPoint.y + FloatingColliderOffset;
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        /*if(isGrounded && !isJumping)
        {
            transform.position = targetPosition;

            /*if (_playerManager.isInteracting || _manager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / .1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }*/
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            _animatorManager._animator.SetBool("isJumping", true);
            _animatorManager.PlayTargetAnimation("EmptyJump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = _moveDirection;
            playerVelocity.y = jumpingVelocity;
            _rigidbody.velocity = playerVelocity;
        }
    }

}
