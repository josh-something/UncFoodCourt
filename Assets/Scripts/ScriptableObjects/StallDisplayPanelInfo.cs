using UnityEngine;
[CreateAssetMenu(fileName = "StallDisplay", menuName = "Scriptable Objects/UI/StallPanelInfo", order = 0)]
public class StallDisplayPanelInfo : ScriptableObject
{
    public Texture StallImage;
    public string StallName;
    [TextArea]public string StallDescription;
    public float StallPrice;
}
