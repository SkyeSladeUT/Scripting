using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpot
{
    private OrderedDrag _objectInSpot;
    public OrderedDrag ObjectInSpot
    {
        get { return _objectInSpot; }
        set
        {
            _objectInSpot = value;
            if (value != null)
            {
                value.transform.position = transform.position;
            }
        }
    }

    public Transform transform
    {
        get { return _gameObject.transform; }
    }

    private GameObject _gameObject;
    public GameObject gameObject
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }

    private int _num;
    public int Num
    {
        get { return _num; }
    }

    public bool Filled
    {
        get { return _objectInSpot != null; }
    }

    public OrderSpot(int num)
    {
        _num = num;
    }

    public bool CorrectObject()
    {
        if(_objectInSpot != null)
        {
            Debug.Log("Order Spot: " + Num + "  Object Num: " + _objectInSpot.CorrectOrder);
            if(_objectInSpot.CorrectOrder == Num)
            {
                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        string printString = "Ordered Spot Object\n";
        printString += "Number of Spots: " + Num + "\n";
        printString += "Object in Spot: " + ObjectInSpot + "\n";
        return printString;
    }

}
