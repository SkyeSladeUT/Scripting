using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClicks : MonoBehaviour
{
    private Camera _cam;
    private IClickable currentClicked;
    private InputMaster _input;
    public InputMaster Input
    {
        get
        {
            if (_input == null)
                _input = new InputMaster();
            return _input;
        }
    }

    private void Awake()
    {
        _cam = Camera.main;
        Input.Simple.LeftMouse.performed += ctx =>
        {
            RaycastHit hit;
            Vector3 coor = Mouse.current.position.ReadValue();
            if (Physics.Raycast(_cam.ScreenPointToRay(coor), out hit))
            {
                currentClicked = hit.collider.GetComponent<IClickable>();
                currentClicked?.OnMouseDown();
            }
        };
        Input.Simple.LeftMouse.canceled += ctx =>
        {
            currentClicked?.OnMouseUp();
            currentClicked = null;
        };
        Input.Simple.Enable();
    }

    private void Update()
    {
        if (currentClicked == null)
            return;
        currentClicked?.OnMouseDrag();
    }
}
