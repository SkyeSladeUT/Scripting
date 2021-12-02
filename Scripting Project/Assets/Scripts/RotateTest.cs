using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 10, 0), Space.Self);
    }
}
