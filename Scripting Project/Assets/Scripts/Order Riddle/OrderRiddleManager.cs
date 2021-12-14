using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OrderRiddleManager : MonoBehaviour
{
    public int NumberOfSpots;
    private List<GameObject> _orderSpots;
    private List<OrderedDrag> _draggables;
    public Grid_Layout_3D GridParent;
    public float CellSize = .1f;
    private OrderedGrid grid;
    public List<GameObject> DraggableObjects;
    public GameObject OrderSpotPrefab;
    public UnityEvent OnComplete;
    public float ZTransform;

    public void Initialize()
    {
        grid = new OrderedGrid(NumberOfSpots);
        _orderSpots = new List<GameObject>();
        for(int i = 0; i < NumberOfSpots; i++)
        {
            if (OrderSpotPrefab != null)
            {
                GameObject temp = Instantiate(OrderSpotPrefab, GridParent.transform);
                _orderSpots.Add(temp);
                grid[i].gameObject = temp;
            }
            else
            {
                GameObject temp = new GameObject("Ordered Spot: " + i);
                _orderSpots.Add(temp);
                temp.transform.parent = GridParent.transform;
                BoxCollider col = temp.AddComponent<BoxCollider>();
                col.size = new Vector3(CellSize, CellSize, .01f);
                col.isTrigger = true;
                grid[i].gameObject = temp;
            }
        }
        GridParent.FixedRowColumnCount = NumberOfSpots;
        GridParent.CellSize.x = CellSize;
        GridParent.CellSize.y = CellSize;
        GridParent.Setup();
        _draggables = new List<OrderedDrag>();
        for(int i = 0; i < DraggableObjects.Count; i++)
        {
            OrderedDrag drag = DraggableObjects[i].GetComponent<OrderedDrag>();
            if(drag == null)
            {
                drag = DraggableObjects[i].AddComponent<OrderedDrag>();
            }
            _draggables.Add(drag);
            drag.OrderObject = new OrderedObject(drag.gameObject);
            drag.OrderObject.Num = i;
            drag.onBeginDrag = OnBeginDrag;
            drag.onEndDrag = OnEndDrag;
            drag.ZTransform = ZTransform;
        }
    }

    public void OnBeginDrag(DragEventArgs e)
    {
        foreach (var p in grid.OrderSpots)
        {
            if (p.Filled && (p.ObjectInSpot.name == e.draggable.gameObject.name))
            {
                p.ObjectInSpot = null;
                break;
            }
        }
    }

    public void OnEndDrag(DragEventArgs e)
    {
        OrderSpot spot = GetSpot(e.draggable.transform);
        if (spot == null)
        {
            return;
        }
        if (spot.Filled)
        {
            OrderedDrag orderedObject;
            if ((orderedObject = e.draggable as OrderedDrag) != null)
            {
                orderedObject.ResetPosition();
            }
            return;
        }
        spot.ObjectInSpot = e.draggable as OrderedDrag;
        CheckOrder();
    }

    public void OnFinishPuzzle()
    {
        OnComplete.Invoke();
    }

    public OrderSpot GetSpot(Transform obj)
    {
        RaycastHit[] hits = Physics.RaycastAll(obj.transform.position, Camera.main.transform.forward, 10);
        for (int i = 0; i < hits.Length; i++)
        {
            foreach (var p in grid.OrderSpots)
            {
                if (p.gameObject.name == hits[i].collider.name)
                {
                    return p;
                }
            }
        }
        return null;
    }

    public bool CheckOrder()
    {
        Debug.Log("Check Order");
        foreach(var o in grid.OrderSpots)
        {
            if (!o.CorrectObject())
                return false;
        }
        OnFinishPuzzle();
        return true;
    }
}
