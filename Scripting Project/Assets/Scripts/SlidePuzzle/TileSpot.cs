using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpot
{
    private int _column, _row;
    public int Column
    {
        get { return _column; }
    }
    public int Row
    {
        get { return _row; }
    }

    private GameObject _objectInSpot;
    public GameObject ObjectInSpot
    {
        get { return _objectInSpot; }
        set 
        { 
            _objectInSpot = value;
            _objectInSpot.transform.position = transform.position;
        }
    }

    private GameObject _gameObject;
    public GameObject gameObject
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }

    public Transform transform
    {
        get { return gameObject.transform; }
    }

    public bool Filled
    {
        get { return _objectInSpot != null; }
    }

    public TileSpot(int column, int row)
    {
        _column = column;
        _row = row;
    }

    public override string ToString()
    {
        string printString = "Tile Spot";
        printString += "Column: " + Column + "  Row: " + Row;
        return printString;
    }
}
