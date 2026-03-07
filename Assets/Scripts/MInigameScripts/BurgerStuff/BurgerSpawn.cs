using UnityEngine;

public class BurgerSpawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] burgerParts;
    private int partsLeft;
    public Transform spawnPoint;
    bool partSpawned;

    //spawns burger parts

    void Start()
    {
        partsLeft = burgerParts.Length;
        partSpawned = false;

        for(int i = 0;i < burgerParts.Length; i++)
        {   
            burgerParts[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (!partSpawned && partsLeft > 0)
        {
            SpawnPart();
        }
    }

    public void SpawnPart()
    {
        if(partsLeft > 0)
        {
            partsLeft--;
            burgerParts[partsLeft].SetActive(true);
            burgerParts[partsLeft].transform.position = spawnPoint.position;
            partSpawned = true;
        }
    }
}
