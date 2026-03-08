using UnityEngine;

[CreateAssetMenu(fileName = "StallFoodData", menuName = "Scriptable Objects/StallFoodData")]
public class StallFoodData : ScriptableObject
{
    [Header("Food Info")]
    public string stallFoodID;         
    public string stallFoodName;          

    [TextArea]
    public string stallFoodDescription;

    [Header("Visual")]
    public Sprite stallFoodImage;
    public Sprite stallFoodIcon;

    [Header("Economy")]
    public int unlockPrice;            
    public float baseIncome;   

    [Header("Stall Settings")]
    public int maxStock = 10;
    public float upgradeMultiplier = 0.25f;

    [Header("For minigames")]
    public MinigameType minigameType;
    
}

public enum MinigameType
{
    Burger,
    Sundae,
    Pizza
}
