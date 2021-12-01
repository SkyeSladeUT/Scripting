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
        for (int i = 0; i < _numColumns; i++)
        {
            for (int j = 0; j < _numRows; j++)
            {
                _tileSpots[i, j] = new TileSpot(i, j);
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



}
