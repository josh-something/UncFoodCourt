using UnityEngine;
using System.Collections;

public class ScoopSpawner : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 2f;
    public float maxSpeed = 6f;
    public float screenPadding = 0.5f;

    [Header("Spawning")]
    public GameObject objectToSpawn;
    public float spawnHeightOffset = 2f;
    public float spawnInterval = 2f;

    private float currentSpeed;
    private bool movingRight = true;
    private Vector2 screenBounds;
    private GameObject[] Scoops;
    private int scoopsToSpawn;
    private Camera miniGameCamera;

    void Start()
    {
        // Find camera dynamically (for additive scene)
        miniGameCamera = GameObject.FindGameObjectWithTag("MiniGameCam")?.GetComponent<Camera>();
        if (miniGameCamera == null)
        {
            Debug.LogError("MiniGameCam not found! Make sure the camera in Sundae scene is tagged correctly.");
            return;
        }

        Scoops = GameObject.FindGameObjectsWithTag("Scoop");
        scoopsToSpawn = Scoops.Length;

        float distance = Mathf.Abs(miniGameCamera.transform.position.z - transform.position.z);
        screenBounds = miniGameCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, distance)
        );

        SetRandomSpeed();

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        MoveLeftRight();
    }

    void MoveLeftRight()
    {
        float moveStep = currentSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveStep);

            // Clamp position so spawner doesn't go off camera
            if (transform.position.x >= screenBounds.x - screenPadding)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveStep);

            if (transform.position.x <= -screenBounds.x + screenPadding)
                movingRight = true;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnObject();
            SetRandomSpeed();
        }
    }

    void SpawnObject()
    {
        if (scoopsToSpawn <= 0) return;

        scoopsToSpawn--;

        Vector3 spawnPos = new Vector3(
            transform.position.x,
            transform.position.y + spawnHeightOffset,
            0f
        );

        // Clamp spawn X to camera bounds
        float distance = Mathf.Abs(miniGameCamera.transform.position.z - spawnPos.z);
        Vector3 min = miniGameCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 max = miniGameCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        spawnPos.x = Mathf.Clamp(spawnPos.x, min.x, max.x);

        GameObject scoop = Scoops[scoopsToSpawn];
        scoop.transform.position = spawnPos;

        Rigidbody2D rb = scoop.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;   // reset movement
            rb.angularVelocity = 0f;
        }
    }

    void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }
}