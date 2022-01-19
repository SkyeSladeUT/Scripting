using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawObject
{
    public JigsawObject()
    {
        CorrectPlacement = false;
    }

    public bool CorrectPlacement;
    public void Correct()
    {
        CorrectPlacement = true;
    }
}
