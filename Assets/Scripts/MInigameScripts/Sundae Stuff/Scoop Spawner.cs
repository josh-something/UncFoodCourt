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

    void Start()
    {
        Camera cam = Camera.main;
        Scoops = GameObject.FindGameObjectsWithTag("Scoop");
        scoopsToSpawn = Scoops.Length;
        float distance = Mathf.Abs(cam.transform.position.z);
        screenBounds = cam.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, distance)
        );

        SetRandomSpeed();

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        // MoveLeftRight();
    }

    void MoveLeftRight()
    {
        float moveStep = currentSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveStep);

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
            SetRandomSpeed(); // change speed every spawn
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