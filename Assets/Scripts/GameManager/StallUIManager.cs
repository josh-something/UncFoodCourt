using UnityEngine;
using TMPro;

public class StallUIManager : MonoBehaviour
{
    public static StallUIManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject unlockPanel;
    public GameObject foodPanel;

    [Header("Unlock UI")]
    public TMP_Text unlockText;

    private StallArea currentStall;

    public void Awake()
    {
        Instance = this;
    }

    public void OpenUnlockPanel(StallArea stall) //Locked Stall Clicked
    {
        UIManager.Instance.OpenBackgroundOverlay();
        currentStall = stall;
        unlockText.text = "Price: " + stall.stallAreaData.unlockPrice.ToString("N0");
        unlockPanel.SetActive(true);
    }

    public void ConfirmUnlock() //Confirm Unlock Button Clicked
    {
        if (StatsManager.Instance.coins >= currentStall.stallAreaData.unlockPrice)
        {
            StatsManager.Instance.coins -= currentStall.stallAreaData.unlockPrice;
            currentStall.UnlockStall();
        }

        unlockPanel.SetActive(false);
        UIManager.Instance.CloseBackgroundOverlay();
    }

    public void CancelUnlock() // Exit Buttone
    {
        unlockPanel.SetActive(false);
        UIManager.Instance.CloseBackgroundOverlay();
    }    

    public void OpenFoodSelection(StallArea stall) //Unlocked but Empty Stall Clicked
    {
        currentStall = stall;
        foodPanel.SetActive(true);
        UIManager.Instance.OpenBackgroundOverlay();
    }

    public void SelectFood(StallFoodData food) //Food Option Clicked
    {
        currentStall.AssignFood(food);
        foodPanel.SetActive(false);
        UIManager.Instance.CloseBackgroundOverlay();
    }

    public void CloseFoodSelection() //Exit Button Clicked
    {
        foodPanel.SetActive(false);
        UIManager.Instance.CloseBackgroundOverlay();
    }

}
