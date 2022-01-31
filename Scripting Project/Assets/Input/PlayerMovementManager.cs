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
    public float TerminalVelocity = 100;

    public float RaycastHeightOffset = .1f;
    public float RaycastRadius = .1f;
    public float RaycastMaxDistance = .5f;
    public float FloatingColliderOffset = 0;

    private float _airTimer;
    #endregion

    #region MOVEMENT VARIABLES
    [Header("Movement Speeds")]
    public float SlowCrawlSpeed = .5f;
    public float FastCrawlSpeed = 1;
    public float WalkingSpeed = 1;
    public float RunningSpeed = 2;
    public float SprintingSpeed = 5;
    public float RotationSpeed = 50;
    #endregion

    #region JUMP VARIABLES
    [Header("Jump")]
    public float GravityIntensity = -15;
    public float JumpHeight = 3;
    public float JumpMovementSpeed = .2f;
    public float JumpWaitTime;
    #endregion

    [SerializeField]
    private bool _isGrounded;
    private bool _isCrawling;

    private Vector3 _moveDirection;
    private Transform _cameraObject;
    private Rigidbody _rigidbody;

    private bool jumpInit;

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
        if (jumpInit)
            _rigidbody.velocity = Vector3.zero;
        if (PlayerAnimation.isInteracting)
        {
            MovePlayer();
            HandleRotation();
            return;
        }
        HandleMovement();
        HandleRotation();
        PlayerAnimation.UpdateMovementValues(GetMoveAmount());
    }

    private void MovePlayer()
    {
        Vector3 playerVelocity = _cameraObject.forward * InputVariables.VerticalInput * JumpMovementSpeed;
        playerVelocity += _cameraObject.right * InputVariables.HorizontalInput * JumpMovementSpeed;
        playerVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = playerVelocity;
    }

    private void HandleMovement()
    {
        if(jumpInit)
            return;
        if (PlayerAnimation.isJumping)
        {
            MovePlayer();
            return;
        }
        if (InputVariables.Crouching)
        {
            if (!_isCrawling)
            {
                //Lower
                PlayerAnimation.PlayTargetAnimation("Stand Down", true);
                _isCrawling = true;
                return;
            }
        }
        else
        {
            if (_isCrawling)
            {
                //Stand Back Up
                PlayerAnimation.PlayTargetAnimation("Stand Up", true);
                _isCrawling = false;
                return;
            }          
        }
        _moveDirection = _cameraObject.forward * InputVariables.VerticalInput;
        _moveDirection += (_cameraObject.right * InputVariables.HorizontalInput);
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        float moveAmount = GetMoveAmount();
        if (InputVariables.Sprinting && moveAmount > .5f)
            _moveDirection *= SprintingSpeed;
        else if (InputVariables.Crouching)
        {
            if (moveAmount >= .5f)
                _moveDirection *= FastCrawlSpeed;
            else
                _moveDirection *= SlowCrawlSpeed;
        }
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

    private void HandleRotation()
    {
        if (jumpInit)
            return;
        if (PlayerAnimation.isJumping)
        {
            Vector3 direction = Vector3.zero;
            direction = _cameraObject.forward * InputVariables.VerticalInput;
            direction += (_cameraObject.right * InputVariables.HorizontalInput);
            direction.Normalize();
            direction.y = 0;

            if (direction == Vector3.zero)
                direction = transform.forward;

            Quaternion rotation = Quaternion.LookRotation(direction);
            Quaternion protation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

            transform.rotation = protation;
            return;
        }
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

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        rayCastOrigin.y += RaycastHeightOffset;
        if(!_isGrounded && !PlayerAnimation.isJumping && !jumpInit)
        {
            if (!PlayerAnimation.isInteracting)
            {
                PlayerAnimation.PlayTargetAnimation("Falling", true);
            }
            _airTimer += Time.deltaTime;
            _rigidbody.AddForce(transform.forward * ForwardFallVelocity);
            Vector3 fallVelocity = -Vector3.up * FallingVelocity * _airTimer;
            if (fallVelocity.y > TerminalVelocity)
                fallVelocity.y = TerminalVelocity;
            _rigidbody.AddForce(fallVelocity);
        }
        if (Physics.SphereCast(rayCastOrigin, RaycastRadius, -Vector3.up, out hit, RaycastMaxDistance, GroundLayer))
        {
            if (!_isGrounded && !_playerAnimation.isInteracting)
            {
                PlayerAnimation.PlayTargetAnimation("Landing", true);
            }
            Vector3 RaycastHitPoint = hit.point;
            targetPosition.y = RaycastHitPoint.y + FloatingColliderOffset;
            _airTimer = 0;
            _isGrounded = true;
            PlayerAnimation.isGrounded = _isGrounded;
        }
        else
        {
            _isGrounded = false;
            PlayerAnimation.isGrounded = _isGrounded;
        }

        if(_isGrounded && !PlayerAnimation.isJumping)
        {
            if (PlayerAnimation.isInteracting || GetMoveAmount() > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / .1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if (_isGrounded && !jumpInit && !InputVariables.Crouching)
        {
            jumpInit = true;
            PlayerAnimation.PlayTargetAnimation("Jump", false);
            StartCoroutine(JumpWait());
        }
    }

    private IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(JumpWaitTime);
        PlayerAnimation.isJumping = true;
        Debug.Log("Jump");
        jumpInit = false;
        float jumpingVelocity = Mathf.Sqrt(-2 * GravityIntensity * JumpHeight);
        Vector3 playerVelocity = _moveDirection;
        playerVelocity.y = jumpingVelocity;
        _rigidbody.velocity = playerVelocity;
    }

    private float GetMoveAmount()
    {
        return Mathf.Clamp01(Mathf.Abs(InputVariables.HorizontalInput) + Mathf.Abs(InputVariables.VerticalInput));
    }

    private void OnDrawGizmos()
    {
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += RaycastHeightOffset;
        Gizmos.DrawWireSphere(rayCastOrigin, RaycastRadius);
    }
}
