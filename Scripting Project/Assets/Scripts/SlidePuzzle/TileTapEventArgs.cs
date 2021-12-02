using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileTapEventArgs : EventArgs
{
    public TileTap tapped { get; set; }
    public TileObject tile { get { return tapped.Tile; } }

    public TileTapEventArgs(TileTap tapped)
    {
        this.tapped = tapped;
    }
}
