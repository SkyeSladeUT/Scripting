using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeManager : MonoBehaviour
{
    private PipeGrid grid;
    [SerializeField]
    private int Columns, Rows;
    private List<GameObject> _spotObjects;
    public Grid_Layout_3D GridParent;
    public float CellSize;
    public List<GameObject> DraggableObjects;
    private List<Pipe_Drag_3D> _draggables;

    public void Initialize()
    {
        grid = new PipeGrid(Columns, Rows);
        _spotObjects = new List<GameObject>();
        for(int j = 0; j < Rows; j++)
        {
            for(int i = 0; i < Columns; i++)
            {
                GameObject temp = new GameObject("C" + i.ToString() + "_R" + j.ToString());
                _spotObjects.Add(temp);
                temp.transform.parent = GridParent.transform;
                BoxCollider col = temp.AddComponent<BoxCollider>();
                col.size = new Vector3(CellSize, CellSize, .01f);
                col.isTrigger = true;
                grid[i, j].Collider = col;
                grid[i, j].name = temp.name;
            }
        }
        GridParent.FixedRowColumnCount = Columns;
        GridParent.CellSize.x = CellSize;
        GridParent.CellSize.y = CellSize;
        GridParent.Setup();
        _draggables = new List<Pipe_Drag_3D>();
        for(int i = 0; i < DraggableObjects.Count; i++)
        {
            Pipe_Drag_3D drag = DraggableObjects[i].GetComponent<Pipe_Drag_3D>();
            if(drag == null)
            {
                drag = DraggableObjects[i].AddComponent<Pipe_Drag_3D>();
            }
            _draggables.Add(drag);
            drag.onBeginDrag = OnBeginDrag;
            drag.onEndDrag = OnEndDrag;
            drag.PipeObject = new PipeObject();
        }
    }

    public void OnBeginDrag(DragEventArgs e)
    {
        Debug.Log("Begin Drag: " + e.draggable.name);
        foreach(var p in grid.PipeSpots)
        {
            if(p.Filled && (p.ObjectInSpot.name == e.draggable.gameObject.name))
            {
                p.ObjectInSpot = null;
                break;
            }
        }
    }

    public void OnEndDrag(DragEventArgs e)
    {
        Debug.Log("End Drag: " + e.draggable.name);
        PipeSpotObject spot = GetSpot(e.draggable.transform);
        if(spot == null)
        {
            return;
        }
        if (spot.Filled)
        {
            Pipe_Drag_3D pipe;
            if ((pipe = e.draggable as Pipe_Drag_3D) != null)
            {
                pipe.ResetPosition();
            }
            return;
        }
        spot.ObjectInSpot = e.draggable.gameObject;
    }

    public PipeSpotObject GetSpot(Transform obj)
    {
        RaycastHit[] hits = Physics.RaycastAll(obj.transform.position, Camera.main.transform.forward, 10);
        for(int i = 0; i < hits.Length; i++)
        {
            foreach(var p in grid.PipeSpots)
            {
                if(p.name == hits[i].collider.name)
                {
                    return p;
                }
            }
        }
        return null;
    }

}
