using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    public List<Collider> CrawlColliders;
    private bool crouching = false;

    private void Update()
    {
        if (InputVariables.Crouching && !crouching) 
        {
            crouching = true;
            foreach(var c in CrawlColliders)
            {
                c.enabled = false;
            }
        }
        else if(!InputVariables.Crouching && crouching)
        {
            crouching = false;
            foreach(var c in CrawlColliders)
            {
                c.enabled = true;
            }
        }
    }
}
