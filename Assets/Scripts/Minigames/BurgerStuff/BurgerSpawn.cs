using UnityEngine;

public class BurgerSpawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] burgerParts;
    private int partsLeft;
    public Transform spawnPoint;
    public int TotalParts() => burgerParts.Length;
    private int spawnIndex;
    private bool canSpawn = false;

    //spawns burger parts

    void Start()
    {
        ResetParts();
        // partsLeft = burgerParts.Length;
        // partSpawned = false;

        // for(int i = 0;i < burgerParts.Length; i++)
        // {   
        //     burgerParts[i].SetActive(false);
        // }
    }

    // private void Update()
    // {
    //     if (!partSpawned && partsLeft > 0)
    //     {
    //         SpawnPart();
    //     }
    // }

    public void SpawnPart()
    {
        // if(partsLeft > 0)
        // {
        //     partsLeft--;
        //     burgerParts[partsLeft].SetActive(true);
        //     burgerParts[partsLeft].transform.position = spawnPoint.position;
        //     partSpawned = true;
        // }
        if (canSpawn) return; 
        if (partsLeft <= 0) return;
        if (spawnIndex >= burgerParts.Length) return;

        canSpawn = true;
        GameObject part = burgerParts[spawnIndex];
        part.transform.position = spawnPoint.position;
        part.SetActive(true);

        spawnIndex++;
        partsLeft--;
    }

    public void UnlockSpawn()
    {
        canSpawn = false;
    }

    public void ResetParts()
    {
        // for (int i = 0; i < burgerParts.Length; i++)
        // {
        //     if (burgerParts[i] != null)
        //     {
        //         burgerParts[i].SetActive(false);

        //         Rigidbody2D rb = burgerParts[i].GetComponent<Rigidbody2D>();
        //         if (rb != null)
        //         {
        //             rb.bodyType = RigidbodyType2D.Dynamic;
        //             rb.linearVelocity = Vector2.zero;
        //             rb.angularVelocity = 0f;
        //         }
        //     }
        // }

        // partsLeft = burgerParts.Length;
        // partSpawned = false;



        partsLeft = burgerParts.Length;
        spawnIndex = 0;
        canSpawn = false;

        for (int i = 0; i < burgerParts.Length; i++)
        {
            if (burgerParts[i] != null)
            {
                burgerParts[i].SetActive(false);

                Rigidbody2D rb = burgerParts[i].GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
        }
    }
}
