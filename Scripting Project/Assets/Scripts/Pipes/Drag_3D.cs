using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drag_3D : MonoBehaviour
{
    private Vector3 _offset;
    private float _zPosition;
    private Camera _camera;

    public delegate void OnBeginDrag(DragEventArgs e);
    public delegate void OnEndDrag(DragEventArgs e);

    public OnBeginDrag onBeginDrag;
    public OnEndDrag onEndDrag;

    private void Start()
    {
        _camera = Camera.main;
    }

    protected virtual void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        onBeginDrag?.Invoke(new DragEventArgs(this));
        _zPosition = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        _offset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zPosition;
        return _camera.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + _offset;
    }

    private void OnMouseUp()
    {
        onEndDrag?.Invoke(new DragEventArgs(this));
    }
}
