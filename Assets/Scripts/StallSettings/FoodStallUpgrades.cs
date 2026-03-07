using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodStallUpgrades : MonoBehaviour
{
    private StallFoodData foodData; // Reference to the ScriptableObject containing food data
    private StallArea stallArea; // Reference to the StallArea component
    private PopularityManager popularityManager; // Reference to the PopularityManager component

    [Header("Upgrade Settings")]
    public float baseIncome;

    [Header("Level")]
    [Range(1, 10)]
    public int level = 1;
    public const int maxLevel = 10;

    [Header("Stock Settings")]
    public int currentStock;
    public GameObject restockButton;

    [Header("Payback Settings")]
    private float basePayback = 15f;
    private float paybackGrowth = 8f;


    private void Awake()
    {
        stallArea = GetComponent<StallArea>();
        popularityManager = FindFirstObjectByType<PopularityManager>();
        restockButton.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Z))
        {
            currentStock--;
        }
        if (Keyboard.current.xKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.X))
        {
            currentStock++;
        }
    }

    public float CurrentMultiplier(int level)
    {
        if (stallArea == null || stallArea.assignedFood == null)
            return 1f;

        return 1f + ((level - 1) * stallArea.assignedFood.upgradeMultiplier);
    }

    public int GetUpgradeCost() // Calculate the cost to upgrade based on the current income and payback settings
    {
        if (level >= maxLevel) // No more upgrades available
            return 0;

        float cost = GetIncomePerCustomer() * GetOrdersToPayback();
        return Mathf.RoundToInt(cost);
    }

    public float GetIncomePerCustomer()
    {
        if (stallArea == null || stallArea.assignedFood == null)
            return 0;

        return stallArea.assignedFood.baseIncome * CurrentMultiplier(level);
    }

    public float GetNextIncome()
    {
        if (stallArea == null || stallArea.assignedFood == null)
            return 0;

        if (level >= maxLevel)
            return GetIncomePerCustomer();

        return stallArea.assignedFood.baseIncome * CurrentMultiplier(level + 1);
    }

    public float GetOrdersToPayback()
    {
        return basePayback + ((level - 1) * paybackGrowth);
    }

    public bool TryUpgrade()
    {
        if (level >= maxLevel)
            return false;

        int cost = GetUpgradeCost();

        bool success = StatsManager.Instance.TrySpendCoins(cost);
        if (!success)
        {
            Debug.Log("Not enough coins to upgrade.");
            return false;
        }

        level++;
        Debug.Log("Upgraded to level " + level);
        return true;

    }

    public void ResetProgress()
    {
        level = 1;

        if (stallArea.assignedFood != null)
            currentStock = stallArea.assignedFood.maxStock;
    }

    public bool TryProcessOrder()
    {
        if (stallArea == null || stallArea.assignedFood == null)
            return false;

        if (currentStock <= 0)
        {
            Debug.Log("Out of stock!");


            if (popularityManager != null)
            {
                popularityManager.AddPopularity(-5);
                Debug.Log("Popularity decreased due to stockout. Current popularity: " + popularityManager.PopularityStars() + " stars.");
            }

            return false;
        }

        float income = GetIncomePerCustomer();

        currentStock--;
        StatsManager.Instance.AddCoins(income);
        popularityManager?.AddPopularity(2);

        if (currentStock <= 3)
        {
            OnLowStock();
        }

        Debug.Log("Order processed. Earned: " + income);

        return true;
    }

    public void OnLowStock()
    {
        restockButton.SetActive(true);

    }

}
