using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    [Header("Currency UI")]
    [SerializeField] private TMP_Text coinsText;


    [Header("Energy UI")]
    [SerializeField] private TMP_Text energyText;


    void OnEnable()
    {
        StatsManager.OnCoinChanged += UpdateCoins;
        StatsManager.OnEnergyChanged += UpdateEnergy;
    }

    void OnDisable()
    {
        StatsManager.OnCoinChanged -= UpdateCoins;
        StatsManager.OnEnergyChanged -= UpdateEnergy;
    }

    void Start()
    {
        // Initialize UI with current values
        UpdateCoins(StatsManager.Instance.GetCoins());
        UpdateEnergy(
            StatsManager.Instance.GetEnergy(),
            StatsManager.Instance.GetMaxEnergy()
        );
    }

    void UpdateCoins(float amount)
    {
        coinsText.text = $"{amount:N0}";
    }



    void UpdateEnergy(float current, float max)
    {
        energyText.text = $"{current}/{max}";

    }
}