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
    public Collider Col
    {
        get 
        { 
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
            }
            return _collider; 
        }
    }
    private Transform snapParent;
    private JigsawGrab _grab;

    private JigsawObject _jigsaw;
    public JigsawObject Jigsaw
    {
        get { return _jigsaw; }
    }

    public delegate void OnPieceSnap();
    public OnPieceSnap onPieceSnap;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        snapParent = transform.GetChild(0);
        _grab = GetComponentInParent<JigsawGrab>();
    }

    public void Initialize()
    {
        _jigsaw = new JigsawObject();
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

        //Make transform the top parent
        JigsawPieceCollider topCollider = this;
        List<JigsawPieceCollider> jigsawPieces = new List<JigsawPieceCollider>() { this };
        while (topCollider.transform.parent.GetComponentInParent<JigsawPieceCollider>() != null)
        {
            topCollider = topCollider.transform.parent.GetComponentInParent<JigsawPieceCollider>();
            jigsawPieces.Add(topCollider);
        }



        if (jigsawPieces.Count > 1)
        {
            for(int i = 0; i < jigsawPieces.Count-1; i++)
            {
                jigsawPieces[i]._grab.transform.parent = jigsawPieces[i]._grab.initparent;
            }

            for(int i = jigsawPieces.Count-1; i > 0; i--)
            {
                jigsawPieces[i].SnapCorrect();
            }
        }

        transform.parent.parent = connectedPiece.snapParent;
        transform.parent.localPosition = Vector3.zero;
        this.enabled = false;
        connectedPiece.GetComponent<JigsawPieceCollider>().enabled = false;
        this.enabled = false;
        Jigsaw.Correct();
        connectedPiece.Jigsaw.Correct();
        onPieceSnap?.Invoke();
        return true;
    }

    public void SnapCorrect()
    {
        transform.parent.parent = correctPiece.snapParent;
        transform.parent.localPosition = Vector3.zero;
        this.enabled = false;
        correctPiece.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_grab.grabbed && !Jigsaw.CorrectPlacement)
        {
            if (other.CompareTag("Piece"))
            {
                //Check to see if connections are compatible (if correct piece)
                JigsawPieceCollider connectedPiece = other.GetComponent<JigsawPieceCollider>();
                if (connectedPiece != null && connectedPiece == correctPiece)
                {
                    CheckCollider(connectedPiece);
                }

            }
        }
    }

    public void UpdateRotation()
    {
        switch (Side)
        {
            case ColliderSide.Bottom:
                Side = ColliderSide.Right;
                break;
            case ColliderSide.Left:
                Side = ColliderSide.Bottom;
                break;
            case ColliderSide.Right:
                Side = ColliderSide.Top;
                break;
            case ColliderSide.Top:
                Side = ColliderSide.Left;
                break;
        }
    }
}
