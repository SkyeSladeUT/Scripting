using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class JigsawGrab : Drag_3D
{
    private float clickTime;
    private float minDragTime = .1f;
    private bool mouseDown = false;
    public JigsawPieceCollider topSide, rightSide, leftSide, bottomSide;
    private GameObject rotateTemp;

    private JigsawObject _jigsaw;
    public JigsawObject JigsawPiece
    {
        get { return _jigsaw; }
        set { _jigsaw = value; }
    }

    protected override void OnMouseDown()
    {
        mouseDown = true;
        clickTime = 0;
        base.OnMouseDown();
    }

    protected override void OnMouseUp()
    {
        mouseDown = false;
        if (clickTime < minDragTime)
        {
            RotatePiece();
        }
    }

    private void FixedUpdate()
    {
        if (mouseDown)
        {
            clickTime += Time.deltaTime;
        }
    }

    private void RotatePiece()
    {
        var temp = topSide.Side;
        topSide.Side = leftSide.Side;
        leftSide.Side = bottomSide.Side;
        bottomSide.Side = rightSide.Side;
        rightSide.Side = temp;
        if(rotateTemp == null)
        {
            rotateTemp = new GameObject();
            rotateTemp.transform.rotation = transform.rotation;
        }
        Quaternion initRotation = rotateTemp.transform.rotation;
        rotateTemp.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        Quaternion newRotation = rotateTemp.transform.rotation;
        gameObject.Tween(gameObject.name + "Rotate", initRotation, newRotation, .1f, TweenScaleFunctions.CubicEaseInOut, (t)=>
        {
            gameObject.transform.rotation = t.CurrentValue;
        });
    }
}
