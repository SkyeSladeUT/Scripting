using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimatorManager : MonoBehaviour
{
    [HideInInspector]
    public Animator _animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //this will directly reference the horizontal parameter of the animator
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Animation Snapping
        #region SNAPPED HORIZONTAL
        float snappedHorizontal;
        if (horizontalMovement > 0 && horizontalMovement < .55f)
            snappedHorizontal = .5f;
        else if(horizontalMovement > .55f)
            snappedHorizontal = 1;
        else if(horizontalMovement < 0 && horizontalMovement > -.55f)
            snappedHorizontal = -.5f;
        else if(horizontalMovement < -.55f)
            snappedHorizontal = -1;
        else
            snappedHorizontal = 0;
        #endregion
        #region SNAPPED VERTICAL
        float snappedVertical;
        if (verticalMovement > 0 && verticalMovement < .55f)
            snappedVertical = .5f;
        else if (verticalMovement > .55f)
            snappedVertical = 1;
        else if (verticalMovement < 0 && verticalMovement > -.55f)
            snappedVertical = -.5f;
        else if (verticalMovement < -.55f)
            snappedVertical = -1;
        else
            snappedVertical = 0;
        #endregion

        if (isSprinting)
        {
            snappedVertical = 2;
        }

        _animator.SetFloat(horizontal, snappedHorizontal, .1f, Time.deltaTime);
        _animator.SetFloat(vertical, snappedVertical, .1f, Time.deltaTime);

    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        _animator.SetBool("isInteracting", isInteracting);
        _animator.CrossFade(targetAnimation, .05f);
    }
}
