using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileTap : MonoBehaviour
{
    public delegate void OnTap(TileTapEventArgs e);
    public OnTap onTap;

    private TileObject _tile;
    private int _tileNum;

    private SpriteRenderer _renderer;

    public int TileNum { 
        get { return _tileNum; }
    }

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public TileObject Tile
    {
        get { return _tile; }
        set { _tile = value; }
    }

    public void OnMouseDown()
    {
        onTap?.Invoke(new TileTapEventArgs(this));
    }

    public void SetNum(int num, Sprite sprite)
    {
        _tileNum = num;
        Tile.Num = num;
        _renderer.sprite = sprite;
    }

}
