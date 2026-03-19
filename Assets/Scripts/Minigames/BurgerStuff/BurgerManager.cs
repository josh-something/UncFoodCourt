using System;
using System.Collections;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public static BurgerManager instance;
    public TextMeshProUGUI burgerPlacedTxt;
    // public TextMeshProUGUI burgerCenteredTxt;

    public int placed,centered,missed;
    public BurgerSpawn burgerSpawn;

    public GameObject finishedPanel;

    //manages UI and burger stats


    public void Awake()
    {
        // if (instance == null) instance = this;
        // else Destroy(gameObject);

        // burgerSpawn = GetComponent<BurgerSpawn>();

        if (instance == null) instance = this;
        else Destroy(gameObject);

        burgerSpawn = GetComponent<BurgerSpawn>();
    }

    void OnEnable()
    {
        if (burgerSpawn == null)
            burgerSpawn = GetComponent<BurgerSpawn>();

        if (finishedPanel != null)
            finishedPanel.SetActive(false);

        ResetMinigame();
    }

    void Start()
    {
        if(burgerSpawn != null)Debug.Log("burgerSpawn isn't null");
        UpdateText();
        // burgerSpawn.SpawnPart();
    }
    

    public void UpdateText()
    {
        ChangePlacedText("Placed: ");
        // ChangeCenteredText("Centered: ");
    }
    public void ChangePlacedText(String txtToEnter)
    {
        burgerPlacedTxt.text = txtToEnter + placed;
    }
    // public void ChangeCenteredText(String txtToEnter)
    // {   
    //     burgerCenteredTxt.text = txtToEnter + centered;
    // }
    private bool isSpawning = false;

    // public IEnumerator GetBurgerPart()
    // {
    //     if (isSpawning) yield break;

    //     isSpawning = true;

    //     yield return new WaitForSeconds(1f);

    //     burgerSpawn.SpawnPart();

    //     isSpawning = false;

    //     if (placed + missed < burgerSpawn.TotalParts())
    //     burgerSpawn.SpawnPart();
    // }

    void ResetMinigame()
    {
        StopAllCoroutines();
        isSpawning = false;

        if (burgerSpawn == null) return; 

        burgerSpawn.ResetParts();

        placed = 0;
        centered = 0;
        missed = 0;

        UpdateText();

        burgerSpawn.SpawnPart(); 
    }

    public void CheckMinigameEnd()
    {
        if (placed + missed >= burgerSpawn.TotalParts())
        {
            EndMinigame();
        }
    }

    void EndMinigame()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(1f);
        
        finishedPanel.SetActive(true);
    }

    public IEnumerator GetBurgerPart()
    {
        if (isSpawning) yield break;
        isSpawning = true;

        yield return new WaitForSeconds(2f);

        burgerSpawn.UnlockSpawn();  
        burgerSpawn.SpawnPart();    

        isSpawning = false;
    }
}
