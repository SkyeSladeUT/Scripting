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
    private List<TileTap> _tappables;
    public GameObject TileObject;
    public List<int> numOrder;
    public List<Sprite> sprites;
    public void Initialize()
    {
        grid = new TileGrid(NumberOfColumns, NumberOfRows);
        _spotObjects = new List<GameObject>();
        for (int j = 0; j < NumberOfRows; j++)
        {
            for (int i = 0; i < NumberOfColumns; i++)
            {
                GameObject temp = new GameObject("C" + i.ToString() + "_R" + j.ToString());
                _spotObjects.Add(temp);
                temp.transform.parent = GridParent.transform;
                BoxCollider col = temp.AddComponent<BoxCollider>();
                col.size = new Vector3(CellSize, .01f, CellSize);
                col.isTrigger = true;
                grid[i, j].gameObject = temp;
                temp.transform.localEulerAngles = Vector3.zero;
            }
        }
        GridParent.FixedRowColumnCount = NumberOfColumns;
        GridParent.CellSize.x = CellSize;
        GridParent.CellSize.y = CellSize;
        GridParent.Setup();
        _tappables = new List<TileTap>();
        for (int i = 0; i < grid.TileSpots.Length - 1; i++)
        {
            GameObject tempTile = Instantiate(TileObject);
            tempTile.name = "Tile_" + i;
            TileTap tap = tempTile.GetComponent<TileTap>();
            if(tap == null)
            {
                tap = tempTile.AddComponent<TileTap>();
            }
            tap.Tile = new TileObject(tempTile);
            tap.onTap = OnTap;
            tap.SetNum(numOrder[i], sprites[numOrder[i] - 1]);
            _tappables.Add(tap);
            grid[i].ObjectInSpot = tap;
        }
        TileObject.SetActive(false);
    }

    public void OnTap(TileTapEventArgs e)
    {
        TileSpot tappedSpot = GetSpot(e.tapped.transform);
        if (tappedSpot == null)
            return;
        grid.MoveTile(tappedSpot, e.tapped);
    }

    public TileSpot GetSpot(Transform obj)
    {
        RaycastHit[] hits = Physics.RaycastAll(obj.transform.position - (Camera.main.transform.forward * 1), Camera.main.transform.forward, 10);
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
