using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    #region Variables

    private bool _inFocus = true;
    public GameObject shadedBackgroundGameObj;

    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition;

    #endregion

    private void Awake() 
    {
        _mainCamera = Camera.main;
        EnhancedTouchSupport.Enable();
    } 

    private void Start()
    {
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.max.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.max.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }
    
    public void OnDrag(InputAction.CallbackContext ctx)
    {
        _inFocus = !shadedBackgroundGameObj.activeSelf;

        if (ctx.started)
            _origin = GetPointerPosition();

        _isDragging = ctx.started || ctx.performed;

        if (ctx.canceled)
            _isDragging = false;
    }

    private void LateUpdate()
    {
        if (!_isDragging || !_inFocus) return;
        
        _difference = GetPointerPosition() - transform.position;
        
        _targetPosition = _origin - _difference;
        _targetPosition = GetCameraBounds();

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * 15f);
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetPointerPosition()
    {
        Vector2 screenPos = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPos = Mouse.current.position.ReadValue();
        }

        return _mainCamera.ScreenToWorldPoint(screenPos);
    }
}
