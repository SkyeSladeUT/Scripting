using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPieceCollider : MonoBehaviour
{
    public JigsawPieceCollider correctPiece;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piece"))
        {
            //Check to see if connections are compatible (if correct piece)
            JigsawPieceCollider connectedPiece = other.GetComponent<JigsawPieceCollider>();
            if(connectedPiece == correctPiece)
            {
                //Snap Together
            }
        }
    }
}
