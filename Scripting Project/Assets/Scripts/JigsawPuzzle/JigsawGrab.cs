using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class JigsawGrab : Drag_3D
{
    private float clickTime;
    private float minDragTime = .1f;
    private bool mouseDown = false;
    [Tooltip("Type of side present (0=side, 1=inward, 2=outward)")]
    public int topSide, rightSide, leftSide, bottomSide;
    private GameObject rotateTemp;

    private JigsawObject _jigsaw;
    public JigsawObject JigsawPiece
    {
        get { return _jigsaw; }
    }

    private JigsawPieceCollider _top, _right, _left, _bottom;
    public JigsawPieceCollider Top
    {
        get { return _top; }
    }
    public JigsawPieceCollider Right
    {
        get { return _right; }
    }
    public JigsawPieceCollider Left
    {
        get { return _left; }
    }
    public JigsawPieceCollider Bottom
    {
        get { return _bottom; }
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
        var temp = topSide;
        topSide = leftSide;
        leftSide = bottomSide;
        bottomSide = rightSide;
        rightSide = temp;
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
