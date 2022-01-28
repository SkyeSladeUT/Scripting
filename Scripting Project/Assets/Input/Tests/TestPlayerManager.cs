using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : MonoBehaviour
{
    TestInputManager _manager;
    TestPlayerLocomotion _playerLocomotion;
    //TestCameraManager _cameraManager;
    Animator _animator;

    public bool isInteracting;

    private void Awake()
    {
        _manager = GetComponent<TestInputManager>();
        _playerLocomotion = GetComponent<TestPlayerLocomotion>();
       // _cameraManager = FindObjectOfType<TestCameraManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _manager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        //_cameraManager.HandleAllCameraMovement();

        isInteracting = _animator.GetBool("isInteracting");
        _playerLocomotion.isJumping = _animator.GetBool("isJumping");
        _animator.SetBool("isGrounded", _playerLocomotion.isGrounded);
    }
}
