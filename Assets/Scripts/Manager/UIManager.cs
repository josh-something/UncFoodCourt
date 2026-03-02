using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // public static UIManager Instance;

    // [Header("Panels")]
    // public GameObject unlockPanel;
    // public GameObject assignPanel;

    // [Header("Buy UI")]
    // public TMP_Text costText;

    // // public TMP_Text stallPriceText;
    // // public Button unlockButton;

    // // public bool stallBought;

    // private StallArea currentStall;

    // private void Awake()
    // {
    //     Instance = this;
    // }

    // public void OpenUnlockPanel(StallArea stall)
    // {
    //     currentStall = stall;
    //     costText.text = "Price: " + stall.unlockCost;
    //     unlockPanel.SetActive(true);
    // }

    // public void ConfirmPurchase()
    // {
    //     if (StatsManager.Instance.coins >= currentStall.unlockCost)
    //     {
    //         StatsManager.Instance.TrySpendCoins(currentStall.unlockCost);
    //         currentStall.UnlockStall();
    //         unlockPanel.SetActive(false);
    //     }
    // }

    // public void OpenAssignPanel(StallArea stall)
    // {
    //     currentStall = stall;
    //     assignPanel.SetActive(true);
    // }

    // public void AssignFood(StallFoodData food)
    // {
    //     currentStall.AssignFood(food);
    //     assignPanel.SetActive(false);
    // }

    // public void ClosePanel()
    // {
    //     unlockPanel.SetActive(false);
    //     assignPanel.SetActive(false);
    // }

    // public void OpenStallPanel(StallFoodData food)
    // {
    //     // Implement stall panel opening logic here
    //     Debug.Log("Opening stall panel for: " + food.stallFoodName);
    // }

}
