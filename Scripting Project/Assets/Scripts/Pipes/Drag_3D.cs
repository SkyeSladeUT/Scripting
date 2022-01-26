using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Drag_3D : MonoBehaviour, IClickable
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
        _zPosition = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        _offset = gameObject.transform.position - GetMouseAsWorldPoint();
        //_offset = Vector3.zero;
        onBeginDrag?.Invoke(new DragEventArgs(this));
    }

    protected Vector3 GetMouseAsWorldPoint()
    {
        //Vector3 mousePoint = Input.mousePosition;
        if (LockAxis == ELockAxis.X)
            ZTransform = gameObject.transform.localPosition.x;
        else if (LockAxis == ELockAxis.Y)
            ZTransform = gameObject.transform.localPosition.y;
        else
            ZTransform = gameObject.transform.localPosition.z;
        Vector3 mousePoint = Mouse.current.position.ReadValue();
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

    void IClickable.OnMouseDown()
    {
        this.OnMouseDown();
    }

    void IClickable.OnMouseDrag()
    {
        this.OnMouseDrag();
    }

    void IClickable.OnMouseUp()
    {
        this.OnMouseUp();
    }
}
