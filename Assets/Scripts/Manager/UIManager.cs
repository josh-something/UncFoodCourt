using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unlock Panel")]
    [SerializeField] private GameObject unlockPanel;
    [SerializeField] private TMP_Text stallPriceText;
    [SerializeField] private Button unlockButton;

    public bool stallBought;

    public static UIManager Instance;

    private StallArea currentStall;

    private void Awake()
    {
        Instance = this;
        unlockPanel.SetActive(false);
    }

    public void OpenUnlockPanel(StallArea stall)
    {
        currentStall = stall;

        var info = stall.stallInfo;
        stallPriceText.text = "Price: " + info.StallPrice;

        unlockPanel.SetActive(true);
    }

    public void ConfirmUnlock()
    {
        if (currentStall == null) return;

        if (StatsManager.Instance.TrySpendCoins(currentStall.stallInfo.StallPrice))
        {
            currentStall.UnlockStall();
            ClosePanel();
        }
        else
        {
            Debug.Log("Not enough coins!");
            ClosePanel();

        }
    }

    public void ClosePanel()
    {
        unlockPanel.SetActive(false);
    }


}
