using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTap : MonoBehaviour
{
    public delegate void OnTap(TileTapEventArgs e);
    public OnTap onTap;

    private TileObject _tile;
    public TileObject Tile
    {
        get { return _tile; }
    }

    public void OnMouseDown()
    {
        onTap?.Invoke(new TileTapEventArgs(this));
    }
}
