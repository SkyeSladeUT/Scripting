using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedGrid
{
    private int _numSpots;
    private OrderSpot[] _orderSpots;
    public OrderSpot[] OrderSpots
    {
        get { return _orderSpots; }
    }

    public OrderSpot this[int index]
    {
        get { return _orderSpots[index]; }
        set { _orderSpots[index] = value; }
    }

    public OrderedGrid(int numSpots)
    {
        _numSpots = numSpots;
        Setup();
    }

    public void Setup()
    {
        _orderSpots = new OrderSpot[_numSpots];
        for (int i = 0; i < _numSpots; i++)
        {
            _orderSpots[i] = new OrderSpot(i);
        }
    }

    public override string ToString()
    {
        string printString = "Pipe Grid Object\n";
        printString += "Number of Spots: " + _numSpots + "\n";
        foreach (var o in _orderSpots)
        {
            printString += o.ToString() + "\n";
        }
        return printString;
    }
}
