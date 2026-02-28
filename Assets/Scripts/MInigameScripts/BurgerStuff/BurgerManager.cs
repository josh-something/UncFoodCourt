using System;
using System.Collections;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public static BurgerManager instance;
    public TextMeshProUGUI burgerPlacedTxt,burgerCenteredTxt;
    public int placed,centered,missed;
    BurgerSpawn burgerSpawn;

    public void Awake()
    {
        
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        burgerSpawn = GetComponent<BurgerSpawn>();
        if(burgerSpawn != null)Debug.Log("burgerSpawn isn't null");
        UpdateText();
        burgerSpawn.SpawnPart();
    }
    

    public void UpdateText()
    {
        ChangePlacedText("Placed: ");
        ChangeCenteredText("Centered: ");
    }
    public void ChangePlacedText(String txtToEnter)
    {
        burgerPlacedTxt.text = txtToEnter + placed;
    }
    public void ChangeCenteredText(String txtToEnter)
    {   
        burgerCenteredTxt.text = txtToEnter + centered;
    }
    private bool isSpawning = false;

public IEnumerator GetBurgerPart()
{
    if (isSpawning) yield break;

    isSpawning = true;

    yield return new WaitForSeconds(1f);

    burgerSpawn.SpawnPart();

    isSpawning = false;
}
}
