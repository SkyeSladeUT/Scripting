using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Drag_3D : Drag_3D
{
    private PipeObject _pipeObject;
    public PipeObject PipeObject
    {
        get { return _pipeObject; }
        set { _pipeObject = value; }
    }

    private Vector3 _origPos;
    protected override void OnMouseDown()
    {
        _origPos = transform.position;
        base.OnMouseDown();
    }

    public virtual void ResetPosition()
    {
        transform.position = _origPos;
    }
}
