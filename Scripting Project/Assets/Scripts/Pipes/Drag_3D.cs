using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drag_3D : MonoBehaviour
{
    public enum ELockAxis
    {
        X, Y, Z
    }

    protected Vector3 _offset;
    protected float _zPosition;
    protected Camera _camera;

    public delegate void OnBeginDrag(DragEventArgs e);
    public delegate void OnEndDrag(DragEventArgs e);

    public OnBeginDrag onBeginDrag;
    public OnEndDrag onEndDrag;

    [HideInInspector]
    public float ZTransform = 1;

    public ELockAxis LockAxis = ELockAxis.Z;

    private void Start()
    {
        _camera = Camera.main;
    }

    protected virtual void OnMouseDown()
    {
        onBeginDrag?.Invoke(new DragEventArgs(this));
        _zPosition = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        _offset = Vector3.zero;
    }

    protected Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zPosition;
        return _camera.ScreenToWorldPoint(mousePoint);
    }

    protected virtual void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + _offset;
        Vector3 localz = transform.localPosition;
        if (LockAxis == ELockAxis.X)
            localz.x = ZTransform;
        else if (LockAxis == ELockAxis.Y)
            localz.y = ZTransform;
        else
            localz.z = ZTransform;
        transform.localPosition = localz;
    }

    protected virtual void OnMouseUp()
    {
        onEndDrag?.Invoke(new DragEventArgs(this));
    }
}
