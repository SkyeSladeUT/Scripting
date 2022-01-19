using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class JigsawGrab : Drag_3D
{
    private float clickTime;
    private float minDragTime = .1f;
    private bool mouseDown = false;
    [HideInInspector]
    public bool grabbed = false;
    private GameObject rotateTemp;
    private JigsawGrab currentGrab;

    [HideInInspector]
    public Transform initparent;
    public void Initialize()
    {
        initparent = transform.parent;
        foreach(var p in transform.GetComponentsInChildren<JigsawPieceCollider>())
        {
            p.Initialize();
        }
    }

    protected override void OnMouseDown()
    {
        currentGrab = this;
        while (currentGrab.transform.parent.GetComponentInParent<JigsawGrab>() != null)
        {
            currentGrab = currentGrab.transform.parent.GetComponentInParent<JigsawGrab>();
        }
        mouseDown = true;
        clickTime = 0;
        if (currentGrab != null)
        {
            onBeginDrag?.Invoke(new DragEventArgs(this));
            _zPosition = _camera.WorldToScreenPoint(currentGrab.transform.position).z;
            _offset = Vector3.zero;
        }
    }

    protected override void OnMouseDrag()
    {
        if(currentGrab != null)
        {
            grabbed = true;
            currentGrab.transform.position = GetMouseAsWorldPoint() + _offset;
            Vector3 localz = currentGrab.transform.localPosition;
            if (LockAxis == ELockAxis.X)
                localz.x = ZTransform;
            else if (LockAxis == ELockAxis.Y)
                localz.y = ZTransform;
            else
                localz.z = ZTransform;
            currentGrab.transform.localPosition = localz;
        }
    }

    protected override void OnMouseUp()
    {
        if (currentGrab != null)
        {
            mouseDown = false;
            if (clickTime < minDragTime)
            {
                RotatePiece();
            }
            else
            {
                StartCoroutine(Check());
            }
            onEndDrag?.Invoke(new DragEventArgs(this));
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
        if (currentGrab != null)
        {
            foreach (var p in GetComponentsInChildren<JigsawPieceCollider>())
            {
                p.UpdateRotation();
            }
            if (rotateTemp == null)
            {
                rotateTemp = new GameObject();
            }
            rotateTemp.transform.rotation = currentGrab.transform.rotation;
            Quaternion initRotation = rotateTemp.transform.rotation;
            rotateTemp.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
            Quaternion newRotation = rotateTemp.transform.rotation;
            gameObject.Tween(gameObject.name + "Rotate", initRotation, newRotation, .1f, TweenScaleFunctions.CubicEaseInOut, (t) =>
            {
                currentGrab.transform.rotation = t.CurrentValue;
            });
            StartCoroutine(Check());
        }
    }

    private IEnumerator Check()
    {
        foreach(var p in GetComponentsInChildren<JigsawPieceCollider>())
        {
            p.Col.enabled = true;
            p.correctPiece.Col.enabled = true;
        }
        yield return new WaitForSeconds(.1f);
        foreach(var p in GetComponentsInChildren<JigsawPieceCollider>())
        {
            p.Col.enabled = false;
            p.correctPiece.Col.enabled = false;
        }
        grabbed = false;
    }

    public bool CheckCorrect()
    {
        foreach(var p in GetComponentsInChildren<JigsawPieceCollider>())
        {
            if (!p.Jigsaw.CorrectPlacement)
            {
                return false;
            }
        }
        return true;
    }
}
