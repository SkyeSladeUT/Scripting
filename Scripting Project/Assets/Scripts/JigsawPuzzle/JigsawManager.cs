using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{
    public List<JigsawGrab> pieces;

    public void Initialize()
    {
        foreach(var p in pieces)
        {
            p.Initialize();
            foreach(var j in p.GetComponentsInChildren<JigsawPieceCollider>())
            {
                j.onPieceSnap = () =>
                {
                    CheckCompletion();
                };
            }
        }
    }

    public bool CheckCompletion()
    {
        foreach(var piece in pieces)
        {
            if (!piece.CheckCorrect())
            {
                return false;
            }
        }
        Debug.Log("Finished");
        return true;
    }
}
