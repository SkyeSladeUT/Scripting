using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PourTapManager : MonoBehaviour
{
    public int MaxAmount;
    public int StartingAmount;
    private TextMeshProUGUI _amountCount;
    private float _fillPercentage;
    private int _currentFill;
    private bool _selected;
    public Renderer rend;


    public bool Selected
    {
        get { return _selected; }
        set 
        { 
            _selected = value;
           // _outline.enabled = _selected;
        }
    }
    private LiquidHolder _holder;
    public LiquidHolder Holder
    {
        get { return _holder; }
        set { _holder = value; }
    }
    public delegate void OnTap(PourTapManager pourManager);

    public OnTap onTap;

    public void UpdateFill(int fillAmount)
    {
        _currentFill = fillAmount;
        _amountCount.text = (_currentFill).ToString();
        _fillPercentage = ((float)_currentFill/ (float)MaxAmount);
        Debug.Log(_fillPercentage);
        rend.material.SetFloat("_FillAmount", _fillPercentage);
    }

    private void OnMouseDown()
    {
        if (onTap != null)
            onTap(this);
    }

    public void Initialize()
    {
        Holder = new LiquidHolder(MaxAmount, StartingAmount)
        {
            onFillUpdate = UpdateFill
        };
        _amountCount = GetComponentInChildren<TextMeshProUGUI>();
        //_outline = GetComponent<Outline02.Outline>();

        UpdateFill(StartingAmount);
    }

}
