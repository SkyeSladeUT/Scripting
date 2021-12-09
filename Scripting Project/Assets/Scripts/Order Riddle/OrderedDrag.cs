using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedDrag : Drag_3D
{
    private int _correctOrder;
    public int CorrectOrder
    {
        get { return _correctOrder; }
    }

    private OrderedObject _orderObject;
    public OrderedObject OrderObject
    {
        get { return _orderObject;}
        set { _orderObject = value; }
    }
}
