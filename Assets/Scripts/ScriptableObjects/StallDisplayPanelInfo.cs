using UnityEngine;
[CreateAssetMenu(fileName = "StallDisplay", menuName = "Scriptable Objects/UI/StallPanelInfo", order = 0)]
public class StallDisplayPanelInfo : ScriptableObject
{
    public string stallID;
    public Sprite StallImage;
    public string StallName;
    [TextArea]public string StallDescription;
    public float StallPrice;
}
