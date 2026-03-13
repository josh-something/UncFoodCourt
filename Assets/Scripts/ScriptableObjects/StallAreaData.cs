using UnityEngine;

[CreateAssetMenu(fileName = "StallAreaData", menuName = "Scriptable Objects/StallAreaData")]
public class StallAreaData : ScriptableObject
{
    [Header("Stall Info")]
    public string stallID;          
    public string stallAreaName;   
    public int unlockPrice;
}
