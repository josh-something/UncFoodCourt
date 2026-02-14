using System;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public static BurgerManager instance;
    public TextMeshProUGUI burgerPlacedTxt,burgerCenteredTxt;
    public int placed,centered;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public void ChangePlacedText(String txtToEnter)
    {
        burgerPlacedTxt.text = txtToEnter + placed;
    }
    public void ChangeCenteredText(String txtToEnter)
    {   
        burgerCenteredTxt.text = txtToEnter + centered;
    }
}
