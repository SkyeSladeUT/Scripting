using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[ExecuteInEditMode]
public class Grid_Layout_3D : MonoBehaviour
{
    public enum direction
    {
        Vertical,
        Horizontal
    }

    public enum alignment
    {
        UpperRight, UpperCenter, UpperLeft,
        MiddleRight, MiddleCenter, MiddleLeft,
        LowerRight, LowerCenter, LowerLeft
    }

    private List<Transform> content;
    public direction GridDirection;
    public alignment GridAlignment;
    public Vector2 CellSize = new Vector2(1, 1);
    public float FixedRowColumnCount = 4;
    public bool update = false;
    public Vector2 spacing = Vector2.zero;

    private void Update()
    {
        if (update)
        {
            update = false;
            Setup();
        }
    }
    public void Setup()
    {
        content = transform.GetComponentsInChildren<Transform>().ToList();
        content.RemoveAt(0);
        Vector3 offsetPosition = Vector3.zero;
        switch (GridAlignment)
        {
            case alignment.UpperLeft:
                break;
            case alignment.UpperCenter:
                offsetPosition.x -= CellSize.x * .5f;
                break;
            case alignment.UpperRight:
                offsetPosition.x -= CellSize.x;
                break;
            case alignment.MiddleLeft:
                offsetPosition.z += CellSize.y * .5f;
                break;
            case alignment.MiddleCenter:
                offsetPosition.x -= CellSize.x * .5f;
                offsetPosition.z += CellSize.y * .5f;
                break;
            case alignment.MiddleRight:
                offsetPosition.x -= CellSize.x;
                offsetPosition.z += CellSize.y * .5f;
                break;
            case alignment.LowerLeft:
                offsetPosition.z += CellSize.y;
                break;
            case alignment.LowerCenter:
                offsetPosition.x -= CellSize.x * .5f;
                offsetPosition.z += CellSize.y;
                break;
            case alignment.LowerRight:
                offsetPosition.x -= CellSize.x;
                offsetPosition.z += CellSize.y;
                break;
        }
        switch (GridDirection)
        {
            case direction.Horizontal:
                for (int i = 0; i < content.Count; i++)
                {
                    int columnNum = (int)(i % FixedRowColumnCount);
                    int rowNum = (int)(i / FixedRowColumnCount);
                    Vector3 newPosition = offsetPosition;
                    newPosition.x -= (CellSize.x * columnNum) + (spacing.x * columnNum);
                    newPosition.z += (CellSize.y * rowNum) + (spacing.y * rowNum);
                    content[i].localPosition = newPosition;
                }
                break;
            case direction.Vertical:
                for (int i = 0; i < content.Count; i++)
                {
                    int rowNum = (int)(i % FixedRowColumnCount);
                    int columnNum = (int)(i / FixedRowColumnCount);
                    Vector3 newPosition = offsetPosition;
                    newPosition.x -= (CellSize.x * columnNum) + (spacing.x * columnNum);
                    newPosition.z += (CellSize.y * rowNum) + (spacing.y * rowNum);
                    content[i].localPosition = newPosition;
                }
                break;
            default:
                break;
        }
    }
}
