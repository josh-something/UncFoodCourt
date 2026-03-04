using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StallManager : MonoBehaviour
{
    private FoodStallUpgrades foodStallUpgrades; // Reference to the FoodStallUpgrades component
    private StallFoodData stallFoodData; // Reference to the ScriptableObject containing food data
    
    public GameObject StallInfoPanelPrefab;
    public TMP_Text levelText;
    public TMP_Text stallNameText;
    public Image stallImage;
    public TMP_Text descriptionText;
    //public TMP_Text stockText;
    public TMP_Text stockValue;
    //public TMP_Text nextLevelText;
    public TMP_Text nextLevelValue;
    //public TMP_Text upgradeCostText;
    public TMP_Text upgradeCostValue;
    public Button upgradeButton;


    //public List<StallUI> stalls = new List<StallUI>();

    void Start()
    {
        upgradeButton.onClick.AddListener(() => OnUpgradeClicked());
        UpdateStallUI();

        // foreach (var s in stalls)
        // {
        //     s.stallNameText.text = s.stallFoodData.stallFoodName;
        //     s.descriptionText.text = s.stallFoodData.stallFoodDescription;
        //     s.stallImage.sprite = s.stallFoodData.stallFoodIcon;

            
        // }
    }

    void Update()
    {
        UpdateStallUI();
    }

    public void UpdateStallUI()
    {
        if (stallFoodData == null) return;

        stallNameText.text = stallFoodData.stallFoodName;
        descriptionText.text = stallFoodData.stallFoodDescription;
        stallImage.sprite = stallFoodData.stallFoodIcon;

        stockValue.text = foodStallUpgrades.currentStock.ToString() + " /10";

        nextLevelValue.text = foodStallUpgrades.GetNextIncome().ToString($"{foodStallUpgrades.GetNextIncome()}/Order");

        if (foodStallUpgrades.level >= FoodStallUpgrades.maxLevel)
        {
            upgradeButton.interactable = false;
            upgradeCostValue.text = "MAX";
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCostValue.text = foodStallUpgrades.GetUpgradeCost().ToString() + " Coins";
        }

        levelText.text = "Level " + foodStallUpgrades.level.ToString();
    }

    void OnUpgradeClicked()
    {
        if (foodStallUpgrades.TryUpgrade())
        {
            Debug.Log($"{stallFoodData.stallFoodName} upgraded to level {foodStallUpgrades.level}");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade " + stallFoodData.stallFoodName);
        }

        UpdateStallUI();
    }
}
