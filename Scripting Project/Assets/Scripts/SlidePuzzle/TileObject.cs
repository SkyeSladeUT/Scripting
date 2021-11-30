using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject
{
    private GameObject _gameObject;
    public GameObject gameObject
    {
        get { return _gameObject; }
    }

    public TileObject (GameObject obj)
    {
        _gameObject = obj;
    }


}
