using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGrid
{
    private int numColumns, numRows;
    private PipeSpotObject[,] _pipeSpots;
    public PipeSpotObject[,] PipeSpots
    {
        get { return _pipeSpots; }
    }

    public PipeSpotObject this[int column, int row]{
        get { return _pipeSpots[column, row]; }
        set { _pipeSpots[column, row] = value; }
    }

    public PipeGrid(int Columns, int Rows)
    {
        numColumns = Columns;
        numRows = Rows;
        Setup();
    }

    public void Setup()
    {
        _pipeSpots = new PipeSpotObject[numColumns, numRows];
        for(int j = 0; j < numRows; j++)
        {
            for(int i = 0; i < numColumns; i++)
            {
                _pipeSpots[i, j] = new PipeSpotObject(i, j);
            }
        }
    }

    public override string ToString()
    {
        string printString = "Pipe Grid Object\n";
        printString += "Number of Columns: " + numColumns + "  Number of Rows: " + numRows + "\n";
        foreach(var p in _pipeSpots)
        {
            printString += p.ToString() + "\n";
        }
        return printString;
    }



}
