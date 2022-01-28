using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager
{
    private Animator _animator;

    public PlayerAnimationManager(Animator anim)
    {
        _animator = anim;
    }

    public bool isInteracting
    {
        get { return _animator.GetBool("isInteracting"); }
        set { _animator.SetBool("isInteracting", value); }
    }

    public bool isJumping
    {
        get { return _animator.GetBool("isJumping"); }
        set { _animator.SetBool("isJumping", value); }
    }

    public bool isGrounded
    {
        get { return _animator.GetBool("isGrounded"); }
        set { _animator.SetBool("isGrounded", value); }
    }

    public void UpdateMovementValues(float speed)
    {
        #region SNAPPED SPEED
        float snappedSpeed = 0;
        if (InputVariables.Sprinting)
            snappedSpeed = 2;
        else if (speed > .55f)
            snappedSpeed = 1;
        else if (speed > 0)
            snappedSpeed = .5f;
        #endregion

        _animator.SetFloat("Speed", snappedSpeed, .1f, Time.deltaTime);
        //_animator.SetFloat("Direction", direction, .1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        _animator.SetBool("isInteracting", isInteracting);
        _animator.CrossFade(targetAnimation, .05f);
    }
}
