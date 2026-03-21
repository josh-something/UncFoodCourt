using UnityEngine;
using UnityEngine.InputSystem;

public class Cone : MonoBehaviour
{
    public Camera miniGameCamera; 
    private bool dragging;
    private float fixedY;
    private Vector3 minX;
    private Vector3 maxX;

    [SerializeField] private GameObject[] caughtScoops;
    private int caughtScoopsCount;
    private InputAction Hold;

    private void Awake()
    {
        Hold = InputSystem.actions.FindAction("Attack");
    }

    private void OnEnable()
    {
        Hold.started += StartDrag;
        Hold.canceled += StopDrag;
    }

    void Start()
    {
        miniGameCamera = GameObject.Find("MinigameCamera").GetComponent<Camera>();
        fixedY = transform.position.y;
        minX = miniGameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxX = miniGameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void Update()
    {
        if (!dragging) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(miniGameCamera.transform.position.z - transform.position.z);

        Vector3 worldPos = miniGameCamera.ScreenToWorldPoint(mousePos);

        worldPos.x = Mathf.Clamp(worldPos.x, minX.x, maxX.x);
        worldPos.y = fixedY;

        transform.position = worldPos;
    }

    // Called from UI EventTrigger
    public void StartDrag(InputAction.CallbackContext ctx)
    {
        dragging = true;
    }

    public void StopDrag(InputAction.CallbackContext ctx)
    {
        dragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scoop"))
        {
            Destroy(other.gameObject);
            caughtScoopsCount++;
            UpdateScoops();
        }
    }

    void UpdateScoops()
    {
        for (int i = 0; i < caughtScoopsCount && i < caughtScoops.Length; i++)
        {
            caughtScoops[i].SetActive(true);
        }
    }
}