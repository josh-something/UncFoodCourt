using UnityEngine;

public class NewCameraDrag : MonoBehaviour
{
    public GameObject shadedBackgroundGameObj;

    private Camera _cam;
    private Vector3 _lastPointerPosition;
    private bool _isDragging;
    private float _minX;
    private float _maxX;
    public float dragSensitivity = 2f;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        float height = _cam.orthographicSize;
        float width = height * _cam.aspect;

        _minX = Globals.WorldBounds.min.x + width;
        _maxX = Globals.WorldBounds.max.x - width;
    }

    private void Update()
    {
        if (shadedBackgroundGameObj != null && shadedBackgroundGameObj.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0)) //manual start
        {
            _lastPointerPosition = GetPointerWorldPosition();
            _isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) //manual end
        {
            _isDragging = false;
        }

        // Drag movement
        if (_isDragging)
        {
            Vector3 currentPointerPosition = GetPointerWorldPosition();
            float deltaX = (_lastPointerPosition.x - currentPointerPosition.x) * dragSensitivity;

            Vector3 newPosition = transform.position;
            newPosition.x += deltaX;

            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 15f);

            _lastPointerPosition = currentPointerPosition;
        }
    }

    private Vector3 GetPointerWorldPosition()
    {
        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPos = _cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
        return worldPos;
    }
}
