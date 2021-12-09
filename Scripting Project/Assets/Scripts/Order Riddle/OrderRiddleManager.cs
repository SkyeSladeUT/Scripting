using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRiddleManager : MonoBehaviour
{
    public int NumberOfSpots;
    public GameObject SpotPrefab;
    private List<GameObject> _orderSpots;
    private List<OrderedDrag> _draggables;
    public Grid_Layout_3D GridParent;
    public float CellSize = .1f;
    private OrderedGrid grid;
    public List<GameObject> DraggableObjects;

    public void Initialize()
    {
        grid = new OrderedGrid(NumberOfSpots);
        _orderSpots = new List<GameObject>();
        for(int i = 0; i < NumberOfSpots; i++)
        {
            GameObject temp = new GameObject("Ordered Spot: " + i);
            _orderSpots.Add(temp);
            temp.transform.parent = GridParent.transform;
            BoxCollider col = temp.AddComponent<BoxCollider>();
            col.size = new Vector3(CellSize, CellSize, CellSize);
            col.isTrigger = true;
            grid[i].gameObject = temp;
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
        }
    }
}
