using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StallPanelItems : MonoBehaviour
{
    public StallDisplayPanelInfo panelInfo;
    public RawImage stallImage;
    public TMP_Text stallName;
    public TMP_Text stallDescription;
    public TMP_Text stallPrice;
    public Button buy;

    void OnValidate()
    {
        if (panelInfo == null)
            return;
        stallImage.texture = panelInfo.StallImage;
        stallName.text = panelInfo.StallName;
        stallDescription.text = panelInfo.StallDescription;
        stallPrice.text = string.Concat("Price: $", panelInfo.StallPrice.ToString("N0"));
    }
    
    
}
