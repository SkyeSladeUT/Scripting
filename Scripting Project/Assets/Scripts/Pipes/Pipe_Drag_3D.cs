using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Pipe_Drag_3D : Drag_3D
{
    private PipeObject _pipeObject;
    public PipeObject PipeObject
    {
        get { return _pipeObject; }
        set { _pipeObject = value; }
    }

    private Vector3 _origPos;

    private float clickTime;
    private float minDragTime = .1f;
    private bool mouseDown = false;

    private GameObject rotateTemp;

    protected override void OnMouseDown()
    {
        _origPos = transform.position;
        clickTime = 0;
        mouseDown = true;
        base.OnMouseDown();
    }

    private void FixedUpdate()
    {
        if (mouseDown)
        {
            clickTime += Time.deltaTime;
        }
    }

    protected override void OnMouseUp()
    {
        mouseDown = false;
        if(clickTime < minDragTime)
        {
            RotatePiece();
        }
        base.OnMouseUp();
    }

    public virtual void ResetPosition()
    {
        transform.position = _origPos;
    }

    private void RotatePiece()
    {
        if (rotateTemp == null)
        {
            rotateTemp = new GameObject();
            rotateTemp.transform.rotation = transform.rotation;
        }
        Quaternion initRotation = rotateTemp.transform.rotation;
        rotateTemp.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        Quaternion newRotation = rotateTemp.transform.rotation;
        gameObject.Tween(gameObject.name + "Rotate", initRotation, newRotation, .1f, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            gameObject.transform.rotation = t.CurrentValue;
        });
    }
}
