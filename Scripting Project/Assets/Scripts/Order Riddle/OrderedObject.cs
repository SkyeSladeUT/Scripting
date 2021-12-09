using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedObject
{
    private int _num;
    public int Num
    {
        get { return _num; }
        set { _num = value; }
    }

    private GameObject _gameObject;
    public GameObject gameObject
    {
        get { return _gameObject; }
    }

    public OrderedObject(GameObject obj)
    {
        _gameObject = obj;
    }
}
