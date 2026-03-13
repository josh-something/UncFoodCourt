using UnityEngine;

public class Cone : MonoBehaviour
{
    public Camera miniGameCamera; // assign the camera rendering to the RenderTexture
    private bool dragging;
    private float fixedY;

    [SerializeField] private GameObject[] caughtScoops;
    private int caughtScoopsCount;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (!dragging) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(miniGameCamera.transform.position.z - transform.position.z);

        Vector3 worldPos = miniGameCamera.ScreenToWorldPoint(mousePos);

        // Clamp X to camera view
        Vector3 min = miniGameCamera.ViewportToWorldPoint(new Vector3(0, 0, mousePos.z));
        Vector3 max = miniGameCamera.ViewportToWorldPoint(new Vector3(1, 1, mousePos.z));

        worldPos.x = Mathf.Clamp(worldPos.x, min.x, max.x);
        worldPos.y = fixedY;

        transform.position = worldPos;
    }

    // Called from UI EventTrigger
    public void StartDrag()
    {
        dragging = true;
    }

    public void StopDrag()
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