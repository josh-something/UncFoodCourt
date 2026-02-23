using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public PopularityManager popularityManager;

    public GameObject customerPrefab;
    public Transform spawnPoint;

    public int currentCustomers = 0;



    private Coroutine spawnRoutine;

    void Start()
    {
        popularityManager = FindObjectOfType<PopularityManager>();
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            SpawnCustomer();
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int stars = popularityManager.PopularityStars();

            int customersToSpawn = GetCustomerCount(stars);
            float spawnInterval = GetSpawnInterval(stars);

            Debug.Log($"Spawning: {customersToSpawn}");
            Debug.Log($"Next spawn in: {spawnInterval}");

            for (int i = 0; i < customersToSpawn; i++)
            {
                SpawnCustomer();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        currentCustomers++;
    }

    int GetCustomerCount(int stars)
    {
        switch (stars)
        {
            case 0: return 1;
            case 1: return 1;
            case 2: return 2;
            case 3: return 2;
            case 4: return 4;
            case 5: return 4;
            default: return 1;
        }
    }

    float GetSpawnInterval(int stars)
    {
        switch (stars)
        {
            case 0: return 10f;
            case 1: return 5f;
            case 2: return 5f;
            case 3: return 3f;
            case 4: return 3f;
            case 5: return 2f;
            default: return 10f;
        }
    }
}
