using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCharacterControl : MonoBehaviour
{
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
}
