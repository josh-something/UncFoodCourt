using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StallPanelItems : MonoBehaviour
{
    public StallDisplayPanelInfo panelInfo;
    public StallManager stallManager;
    public Image stallImage;
    public TMP_Text stallName;
    public TMP_Text stallDescription;
    public TMP_Text stallPrice;
    public Button buy;

    void OnValidate()
    {
        if (panelInfo == null)
            return;
        stallImage.sprite = panelInfo.StallImage;
        stallName.text = panelInfo.StallName;
        stallDescription.text = panelInfo.StallDescription;
        stallPrice.text = string.Concat("Price: $", panelInfo.StallPrice.ToString("N0"));
        buy.onClick.AddListener(BuyItem);
    }

    void Start()
    {
        stallManager = FindFirstObjectByType<StallManager>();
    }

    void BuyItem()
    {
        if (StatsManager.Instance.coins < panelInfo.StallPrice)
        {
            Debug.Log("Not enough coins");
        }
        else
        {
            StatsManager.Instance.coins -= panelInfo.StallPrice;
            stallManager.AddStall(panelInfo);
        }
    }
}
