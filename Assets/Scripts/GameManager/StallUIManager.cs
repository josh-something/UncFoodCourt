using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data.Common;
using Unity.VisualScripting;

public class StallUIManager : MonoBehaviour
{
    public static StallUIManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject unlockPanel;
    public GameObject foodPanel;

    [Header("Unlock UI")]
    public TMP_Text unlockText;

    [Header("Assign Food UI")]
    public Button[] foodButtons;
    public TMP_Text[] nameTexts;
    public TMP_Text[] priceTexts;

    [Header("Stall Info Panel")]
    public GameObject stallInfoPanel;
    public TMP_Text stallLevelText;
    public Image stallIcon;
    public TMP_Text stallNameText;
    public TMP_Text stallDescriptionText;
    public TMP_Text stallStockText;
    public TMP_Text currentIncomeText;
    public TMP_Text nextLevelText;
    public TMP_Text upgradeCostText;
    public Button upgradeButton;

    [Header("Stall Info Panel")]
    public GameObject stallListPanel;
    public StallArea[] stallList;
    public StallListUIPanel[] stallSlots;

    public StallFoodData[] availableFoods;

    private StallFoodData currentFood;
    private StallArea currentStall;
    private FoodStallUpgrades foodStallUpgrades;

    public void Awake()
    {
        Instance = this;
    }

    public void OpenUnlockPanel(StallArea stall) //Locked Stall Clicked
    {
        //UIManager.Instance.OpenBackgroundOverlay();
        currentStall = stall;
        unlockText.text = "Price: " + stall.stallAreaData.unlockPrice.ToString("N0");
        UIManager.Instance.OpenPanel(unlockPanel);
    }

    public void ConfirmUnlock() //Confirm Unlock Button Clicked
    {
        float price = currentStall.stallAreaData.unlockPrice;

        bool success = StatsManager.Instance.TrySpendCoins(price);

        if (success)
        {
            currentStall.UnlockStall();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }

        unlockPanel.SetActive(false);
        UIManager.Instance.CloseBackgroundOverlay();
    }

    public void CancelUnlock() // Exit Buttone
    {
        unlockPanel.SetActive(false);
        UIManager.Instance.CloseCurrentPanel();
    }

    public void OpenFoodSelection(StallArea stall) //Unlocked but Empty Stall Clicked
    {
        currentStall = stall;
        UIManager.Instance.OpenPanel(foodPanel);
        // UIManager.Instance.OpenBackgroundOverlay();

        RefreshFoodUI();
    }

    public void SelectFood(StallFoodData food) //Food Option Clicked
    {
        if (currentStall == null)
            return;

        if (StatsManager.Instance.IsFoodPurchased(food)) // Double-check if food is already purchased
        {
            Debug.Log("Food already purchased!");
            return;
        }

        bool success = StatsManager.Instance.TrySpendCoins(food.unlockPrice); // Attempt to spend coins, returns false if not enough coins

        if (!success)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        StatsManager.Instance.MarkFoodAsPurchased(food); // Mark food as purchased in stats manager
        currentStall.AssignFood(food); // Update stall with new food
        CloseFoodSelection();
    }

    public void CloseFoodSelection() //Exit Button Clicked
    {
        UIManager.Instance.CloseCurrentPanel();
        // UIManager.Instance.CloseBackgroundOverlay();
    }

    void RefreshFoodUI() // Update the food selection UI based on available foods and player stats
    {
        for (int i = 0; i < availableFoods.Length; i++)
        {
            StallFoodData food = availableFoods[i];

            nameTexts[i].text = food.stallFoodName;

            bool alreadyPurchased = StatsManager.Instance.IsFoodPurchased(food);
            bool notEnoughCoins = StatsManager.Instance.GetCoins() < food.unlockPrice;

            if (alreadyPurchased)
            {
                priceTexts[i].text = "Purchased";
                foodButtons[i].interactable = false;
            }
            else
            {
                priceTexts[i].text = "$" + food.unlockPrice.ToString("N0");
                foodButtons[i].interactable = !notEnoughCoins;
            }

            int index = i;

            foodButtons[i].onClick.RemoveAllListeners();
            foodButtons[i].onClick.AddListener(() =>
            {
                SelectFood(availableFoods[index]);
            });
        }
    }

    public void OpenStallInfoPanel(StallArea stall) //Stall with Food Clicked
    {
        currentStall = stall;
        foodStallUpgrades = stall.GetComponent<FoodStallUpgrades>();

        if (stall.assignedFood == null || foodStallUpgrades == null)
            return;

        UIManager.Instance.OpenPanel(stallInfoPanel);

        RefreshStallInfoUI();
    }

    public void RefreshStallInfoUI()
    {
        if (currentStall == null)
            return;

        StallFoodData food = currentStall.assignedFood;
        FoodStallUpgrades upgrades = currentStall.GetComponent<FoodStallUpgrades>();

        if (food == null || upgrades == null)
            return;

        stallLevelText.text = "Level: " + upgrades.level;

        stallIcon.sprite = food.stallFoodIcon;
        stallNameText.text = food.stallFoodName;
        stallDescriptionText.text = food.stallFoodDescription;

        stallStockText.text = $"{upgrades.currentStock}/{food.maxStock}";
        currentIncomeText.text = $"{upgrades.GetIncomePerCustomer()}/Order";
        nextLevelText.text = $"{upgrades.GetNextIncome()}/Order";
        upgradeCostText.text = $"{upgrades.GetUpgradeCost()} coins";

        upgradeButton.interactable = upgrades.level < FoodStallUpgrades.maxLevel;
    }

    public void CloseStallInfo() //Exit Button Clicked
    {
        UIManager.Instance.CloseCurrentPanel();
        // UIManager.Instance.CloseBackgroundOverlay();
        RefreshStallInfoUI();
    }

    public void OnClickUpgrade() //Upgrade Button Clicked
    {

        bool success = foodStallUpgrades.TryUpgrade();

        if (success)
        {
            RefreshStallInfoUI();
        }
        else
        {
            Debug.Log("Not enough coins or max level reached!");
        }
    }

    public void OpenStallListUIPanel()
    {
        UIManager.Instance.OpenPanel(stallListPanel);

        for (int i = 0; i < stallSlots.Length; i++)
        {
            StallArea stall = stallList[i];

            if (stall.assignedFood == null)
            {
                stallSlots[i].EmptyStall();
            }
            else
            {
                stallSlots[i].StallWithFood(stall.assignedFood);
            }
        }
    }

    public void CloseStallListPanel()
    {
        UIManager.Instance.CloseCurrentPanel();
    }

    public void RestockPressed()
    {
        if (!StatsManager.Instance.TrySpendEnergy(1))
        return;

        MinigameManager.Instance.OpenMinigame(currentStall.assignedFood.minigameType);
    }
}
