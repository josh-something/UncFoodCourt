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

    [Header("Economy")]
    public int unlockPrice;            
    public float baseIncome;   
}
