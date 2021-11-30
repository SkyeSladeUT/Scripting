using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleManager : MonoBehaviour
{
    private TileGrid grid;
    [SerializeField]
    private int NumberOfColumns, NumberOfRows;
    private List<GameObject> _spotObjects;
    public Grid_Layout_3D GridParent;
    public float CellSize;
    public List<GameObject> TappableObjects;
    private List<TileTap> _tappables;

    public void Initialize()
    {
        grid = new TileGrid(NumberOfColumns, NumberOfRows);
        _spotObjects = new List<GameObject>();
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
            {
                GameObject temp = new GameObject("C" + i.ToString() + "_R" + j.ToString());
                _spotObjects.Add(temp);
                temp.transform.parent = GridParent.transform;
                Debug.Log("Grid[" + i + "," + j + "]: " + grid[i, j]);
                grid[i, j].gameObject = temp;
            }
        }
        GridParent.FixedRowColumnCount = NumberOfColumns;
        GridParent.CellSize.x = CellSize;
        GridParent.CellSize.y = CellSize;
        GridParent.Setup();
        _tappables = new List<TileTap>();
        for (int i = 0; i < TappableObjects.Count; i++)
        {
            TileTap drag = TappableObjects[i].GetComponent<TileTap>();
            if (drag == null)
            {
                drag = TappableObjects[i].AddComponent<TileTap>();
            }
            _tappables.Add(drag);
            drag.onTap = OnTap;
        }
    }

    public void OnTap(TileTapEventArgs e)
    {
        Debug.Log("Tapped: " + e.tapped.gameObject.name);
        TileSpot tappedSpot = GetSpot(e.tapped.transform);
        if (tappedSpot == null)
            return;
        Debug.Log("Tapped Spot: " + tappedSpot.gameObject.name);
    }

    public TileSpot GetSpot(Transform obj)
    {
        RaycastHit[] hits = Physics.RaycastAll(obj.transform.position, Camera.main.transform.forward, 10);
        for (int i = 0; i < hits.Length; i++)
        {
            foreach (var t in grid.TileSpots)
            {
                if (t.gameObject.name == hits[i].collider.name)
                {
                    return t;
                }
            }
        }
        return null;
    }

}
