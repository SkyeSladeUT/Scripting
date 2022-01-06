using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidHolder
{
    private int _liquidAmount;
    public int LiquidAmount
    {
        get { return _liquidAmount; }
    }

    private int _maxLiquidAmount;
    public int MaxLiquidAmount
    {
        get { return _maxLiquidAmount; }
    }

    public delegate void OnFillUpdate(int fillAmount);
    public OnFillUpdate onFillUpdate;


    public LiquidHolder(int maxLiquidAmount, int startingLiquidAmount)
    {
        _maxLiquidAmount = maxLiquidAmount;
        _liquidAmount = startingLiquidAmount;
    }

    //return leftover liquid amount;
    public int AddLiquid(int liquidAmount)
    {
        int leftoverLiquid = 0;
        _liquidAmount += liquidAmount;
        if(_liquidAmount > MaxLiquidAmount)
        {
            leftoverLiquid = _liquidAmount - MaxLiquidAmount;
            _liquidAmount = MaxLiquidAmount;
        }
        if(onFillUpdate != null)
        {
            onFillUpdate(LiquidAmount);
        }
        return leftoverLiquid;
    }

    //returns current amount of liquid
    public int PourLiquid()
    {
        int pouredLiquid = _liquidAmount;
        _liquidAmount = 0;
        return pouredLiquid;
    }

}
