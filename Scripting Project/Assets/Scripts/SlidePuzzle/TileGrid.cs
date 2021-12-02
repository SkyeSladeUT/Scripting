using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
    private int _numColumns, _numRows;

    private TileSpot[,] _tileSpots;
    
    public TileSpot[,] TileSpots
    {
        get { return _tileSpots; }
        set { _tileSpots = value; }
    }

    public TileSpot this[int column, int row]
    {
        get { return _tileSpots[column, row]; }
        set { _tileSpots[column, row] = value; }
    }

    public TileSpot this[int index]
    {
        get 
        { 
            int column = index % _numColumns;
            int row = (int)(index / _numColumns);
            return _tileSpots[column, row];
        }
        set
        {
            int column = index % _numColumns;
            int row = (int)(index / _numColumns);
            _tileSpots[column, row] = value;
        }
    }

    public TileGrid(int numColumns, int numRows)
    {
        _numColumns = numColumns;
        _numRows = numRows;
        Setup();
    }

    public void Setup()
    {
        _tileSpots = new TileSpot[_numColumns, _numRows];
        for (int j = 0; j < _numRows; j++)
        {
            for (int i = 0; i < _numColumns; i++)
            {
                _tileSpots[i, j] = new TileSpot(i, j, (i + (j*_numColumns)));
            }
        }
    }

    public override string ToString()
    {
        string printString = "Pipe Grid Object\n";
        printString += "Number of Columns: " + _numColumns + "  Number of Rows: " + _numRows + "\n";
        foreach (var t in _tileSpots)
        {
            printString += t.ToString() + "\n";
        }
        return printString;
    }

    public void MoveTile(TileSpot spot, TileTap tileObj)
    {
        int column = spot.Column;
        int row = spot.Row;
        TileSpot newSpot;
        GameObject tile = spot.ObjectInSpot.gameObject;
        if (column != 0)
        {
            newSpot = TileSpots[column - 1, row];
            if (!newSpot.Filled)
            {
                spot.ObjectInSpot = null;
                newSpot.ObjectInSpot = tileObj;
                CheckPlacement();
                return;
            }
        }
        if(column != _numColumns-1)
        {
            newSpot = TileSpots[column + 1, row];
            if (!newSpot.Filled)
            {
                spot.ObjectInSpot = null;
                newSpot.ObjectInSpot = tileObj;
                CheckPlacement();
                return;
            }
        }
        if(row != 0)
        {
            newSpot = TileSpots[column, row-1];
            if (!newSpot.Filled)
            {
                spot.ObjectInSpot = null;
                newSpot.ObjectInSpot = tileObj;
                CheckPlacement();
                return;
            }
        }
        if(row != _numRows - 1)
        {
            newSpot = TileSpots[column, row + 1];
            if (!newSpot.Filled)
            {
                spot.ObjectInSpot = null;
                newSpot.ObjectInSpot = tileObj;
                CheckPlacement();
                return;
            }
        }
        tileObj.Tile.Wiggle();
    }

    public bool CheckPlacement()
    {
        foreach(var s in TileSpots)
        {
            if(s.Index == 8)
            {
                continue;
            }
            else if(!s.Filled || s.ObjectInSpot.TileNum != s.Index+1)
            {
                return false;
            }
        }
        return true;
    }

}
