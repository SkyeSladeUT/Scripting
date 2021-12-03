using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

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
    private int _index;
    public int Index
    {
        get { return _index; }
    }


    private TileTap _objectInSpot;
    public TileTap ObjectInSpot
    {
        get { return _objectInSpot; }
        set 
        { 
            _objectInSpot = value;
            if (_objectInSpot != null)
            {
                gameObject.Tween(gameObject.name + "Move", _objectInSpot.transform.position, transform.position, .25f, TweenScaleFunctions.CubicEaseInOut, (t)=> { _objectInSpot.transform.position = t.CurrentValue; });
                _objectInSpot.transform.rotation = transform.rotation;
            }
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

    public TileSpot(int column, int row, int index)
    {
        _index = index;
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
