using System;
using UnityEngine;

public class DragEventArgs : EventArgs
{
    public Drag_3D draggable { get; set; }

    public DragEventArgs (Drag_3D draggable)
    {
        this.draggable = draggable;
    }
}
