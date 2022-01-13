using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPieceCollider : MonoBehaviour
{
    public enum ColliderSide
    {
        Right, Left, Bottom, Top
    }

    public JigsawPieceCollider correctPiece;
    public ColliderSide Side;
    private Collider _collider;
    public Transform snapParent;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    private bool CheckCollider(JigsawPieceCollider connectedPiece)
    {
        switch (Side)
        {
            case ColliderSide.Right:
                if (connectedPiece.Side != ColliderSide.Left)
                    return false;
                break;
            case ColliderSide.Left:
                if (connectedPiece.Side != ColliderSide.Right)
                    return false;
                break;
            case ColliderSide.Top:
                if (connectedPiece.Side != ColliderSide.Bottom)
                    return false;
                break;
            case ColliderSide.Bottom:
                if (connectedPiece.Side != ColliderSide.Top)
                    return false;
                break;
        }
        //Snap Pieces
        transform.parent = connectedPiece.snapParent;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piece"))
        {
            //Check to see if connections are compatible (if correct piece)
            JigsawPieceCollider connectedPiece = other.GetComponent<JigsawPieceCollider>();
            if(connectedPiece != null && connectedPiece == correctPiece)
            {
                CheckCollider(connectedPiece);               
            }
        }
    }
}
