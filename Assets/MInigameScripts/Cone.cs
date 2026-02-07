using Unity.VisualScripting;
using UnityEngine;

public class Cone : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private float fixedY;

    [SerializeField]
    private GameObject[] caughtScoops;
    private int caughtScoopsCount;
    void Start()
    {
        // Lock the initial Y position
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (dragging)
        {
            // Mouse position with correct depth
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos) + offset;

            // Clamp position to screen bounds
            Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, mousePos.z));
            Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, mousePos.z));

            worldPos.x = Mathf.Clamp(worldPos.x, min.x, max.x);
            worldPos.z = Mathf.Clamp(worldPos.z, min.z, max.z);

            // Lock Y
            worldPos.y = fixedY;

            transform.position = worldPos;
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePos);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Scoop"))
        {
        Destroy(other.gameObject);
        caughtScoopsCount++;
        UpdateScoops();
        }
    }
    void UpdateScoops()
    {   
        for (int i = 0;i < caughtScoopsCount;i++){
        caughtScoops[i].SetActive(true);
        }
    }
}
