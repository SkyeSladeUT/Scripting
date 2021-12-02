using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpotObject
{
    #region PROPERTIES
    private string _name;
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    private GameObject _objectInSpot;
    public GameObject ObjectInSpot
    {
        get { return _objectInSpot; }
        set 
        { 
            _objectInSpot = value;
            if (value != null)
            {
                value.transform.position = Collider.transform.position;
            }
        }
    }

    private Collider _collider;
    public Collider Collider
    {
        get { return _collider; }
        set { _collider = value; }
    }

    private int _row, _column;

    public int Row
    {
        get { return _row; }
        set { _row = value; }
    }

    public int Column
    {
        get { return _column; }
        set { _column = value; }
    }
    #endregion

    public PipeSpotObject(int column, int row)
    {
        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        string printString = "Pipe Spot Object\n";
        printString += "Column: " + Column + " Row: " + Row + "\n";
        printString += "Collider: " + Collider + "\n";
        printString += "Object in Spot: " + ObjectInSpot + "\n";
        return printString;
    }

    public bool Filled
    {
        get { return _objectInSpot != null; }
    }

}
