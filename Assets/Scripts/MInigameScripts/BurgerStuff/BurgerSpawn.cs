using UnityEngine;

public class BurgerSpawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] burgerParts;
    private int partsLeft;
    public Transform spawnPoint;

    void Start()
    {
        burgerParts = GameObject.FindGameObjectsWithTag("Burger");
        partsLeft = burgerParts.Length;

        for(int i = 0;i < burgerParts.Length; i++)
        {
            burgerParts[i].SetActive(false);
        }
    }

    public void SpawnPart()
    {
        if(partsLeft > 0)
        {
        partsLeft--;
        burgerParts[partsLeft].SetActive(true);
        burgerParts[partsLeft].transform.position = spawnPoint.position;
        }
    }
}
