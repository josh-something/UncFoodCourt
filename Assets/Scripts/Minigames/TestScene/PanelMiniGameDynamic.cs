using UnityEngine;

public class PanelMinigameDynamic : MonoBehaviour
{
    [Header("References")]
    public string coneTag = "Cone";       // Tag of the cone
    public string scoopTag = "Scoop";     // Tag of the scoops
    public string minigameCameraTag = "MiniGameCam"; // Tag of the camera in Sundae scene

    private Camera miniGameCamera;
    private Transform cone;
    private float fixedY;
    private bool dragging = false;

    private int caughtScoopsCount = 0;
    private GameObject[] caughtScoops;

    void Update()
    {
        // If camera hasn't been found yet, try to find it dynamically
        if (miniGameCamera == null)
        {
            GameObject camObj = GameObject.FindGameObjectWithTag(minigameCameraTag);
            if (camObj != null)
            {
                miniGameCamera = camObj.GetComponent<Camera>();
            }
            else
            {
                return; // Wait until the camera exists
            }
        }

        // If cone hasn't been found yet, try to find it dynamically
        if (cone == null)
        {
            GameObject coneObj = GameObject.FindGameObjectWithTag(coneTag);
            if (coneObj != null)
            {
                cone = coneObj.transform;
                fixedY = cone.position.y;
            }
            else
            {
                return; // Wait until the cone exists
            }
        }

        // Tap-and-hold anywhere
        if (Input.GetMouseButton(0))
        {
            dragging = true;
            DragCone(Input.mousePosition);
        }
        else
        {
            dragging = false;
        }
    }

    private void DragCone(Vector3 screenPos)
    {
        if (!dragging || cone == null || miniGameCamera == null) return;

        float depth = Mathf.Abs(miniGameCamera.transform.position.z - cone.position.z);
        screenPos.z = depth;

        Vector3 worldPos = miniGameCamera.ScreenToWorldPoint(screenPos);

        // Clamp to camera bounds
        Vector3 min = miniGameCamera.ViewportToWorldPoint(new Vector3(0, 0, depth));
        Vector3 max = miniGameCamera.ViewportToWorldPoint(new Vector3(1, 1, depth));

        worldPos.x = Mathf.Clamp(worldPos.x, min.x, max.x);
        worldPos.y = fixedY;
        worldPos.z = Mathf.Clamp(worldPos.z, min.z, max.z);

        cone.position = worldPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(scoopTag))
        {
            Destroy(other.gameObject);
            caughtScoopsCount++;
            UpdateScoopsUI();
        }
    }

    private void UpdateScoopsUI()
    {
        if (caughtScoops == null || caughtScoops.Length == 0)
        {
            caughtScoops = GameObject.FindGameObjectsWithTag("CaughtScoop");
        }

        for (int i = 0; i < caughtScoopsCount && i < caughtScoops.Length; i++)
        {
            caughtScoops[i].SetActive(true);
        }
    }
}