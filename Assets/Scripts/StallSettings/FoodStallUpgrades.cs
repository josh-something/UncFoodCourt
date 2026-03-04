using UnityEngine;

public class FoodStallUpgrades : MonoBehaviour
{
    public StallFoodData foodData; // Reference to the ScriptableObject containing food data

    [Header("Upgrade Settings")]
    public int currentStock;
    public float baseIncome;

    [Header("Level")]
    [Range(1, 10)]
    public int level = 1;
    public const int maxLevel = 10;

    [Header("Payback Settings")]
    public float basePayback = 15f;
    public float paybackGrowth = 8f;

    private void Start()
    {
        currentStock = foodData.maxStock;
        baseIncome = foodData.baseIncome;
    }

    public float CurrentMultiplier()
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
        return baseIncome * CurrentMultiplier(); 
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
        if (success)
        {
            level++;
            Debug.Log("Upgraded to level " + level);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade.");
        }
        return true;
    }

    

}
