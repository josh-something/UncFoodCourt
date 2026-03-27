using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PizzaBoxing : MonoBehaviour
{
    public Transform pizzaBox;
    public Vector3 startingLocation;

    public bool isBoxed { get; private set; }
    
    // private InputAction _click;
    // private InputAction _drag;
    
    private bool _isDragging;
    private Camera _camera;

    private void Awake()
    {
        // _click = InputSystem.actions["Attack"];
        // _drag = InputSystem.actions["Point"];
        _camera = Camera.main;
    }

    // private void Start()
    // {
    //     _camera = Camera.main;
    // }

    // void OnEnable()
    // {
    //     startingLocation =  transform.position;
    //     _click.performed += ClickOnstarted;
    //     _click.canceled += ClickOncanceled;
    // }

    // void OnDisable()
    // {
    //     _click.performed -= ClickOnstarted;
    //     _click.canceled -= ClickOncanceled;
    // }

    // private void ClickOnstarted(InputAction.CallbackContext obj)
    // {
    //     Debug.Log("ClickOnstarted");
    //     _isDragging = true;
    // }
    
    // private void ClickOncanceled(InputAction.CallbackContext obj)
    // {
    //     if (!_isDragging) return;
    //     Debug.Log("release Click");
    //     _isDragging = false;
    //     var dist = Vector3.Distance(pizzaBox.position, transform.position);
    //     if (MathF.Abs(dist) < 1)
    //     {
    //         isBoxed = true;
    //         transform.position = pizzaBox.position;
    //         this.enabled = false;
    //     }
    //     else
    //     {
    //         transform.position = startingLocation;
    //     }
    // }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
        }

        // Dragging
        if (_isDragging)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 targetpos = _camera.ScreenToWorldPoint(screenPos);

            transform.position = new Vector3(targetpos.x, targetpos.y, transform.position.z);
        }

        // Release
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;

            var dist = Vector3.Distance(pizzaBox.position, transform.position);

            if (Mathf.Abs(dist) < 1f)
            {
                isBoxed = true;
                transform.position = pizzaBox.position;
                this.enabled = false;
            }
            else
            {
                transform.position = startingLocation;
            }
        }
    }
}
