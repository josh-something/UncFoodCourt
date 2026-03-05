using UnityEngine;

public class FoodStallUpgrades : MonoBehaviour
{
    private StallFoodData foodData; // Reference to the ScriptableObject containing food data
    private StallArea stallArea; // Reference to the StallArea component

    [Header("Upgrade Settings")]
    public float baseIncome;

    [Header("Level")]
    [Range(1, 10)]
    public int level = 1;
    public const int maxLevel = 10;

    [Header("Stock Settings")]
    public int currentStock;

    [Header("Payback Settings")]
    private float basePayback = 15f;
    private float paybackGrowth = 8f;

    private void Start()
    {
        currentStock = foodData.maxStock;
        baseIncome = foodData.baseIncome;
    }

    public void SetFood(StallFoodData food)
    {
        foodData = food;

        if (foodData == null)
            return;

        currentStock = foodData.maxStock;
        baseIncome = foodData.baseIncome;

        Debug.Log("Food assigned to upgrade system: " + foodData.stallFoodName);
    }

    public float CurrentMultiplier(int level )
    {
        return 1f + ((level - 1) * foodData.upgradeMultiplier); // Calculate the current multiplier based on the level and upgrade multiplier
        // multiplier increase per level = 0.25f
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
        if (foodData == null)
        return 0;

        if (level >= maxLevel)
        return GetIncomePerCustomer();

        return foodData.baseIncome * CurrentMultiplier(level + 1);
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

    

}
