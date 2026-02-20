using System;
using TMPro;
using UnityEngine;

public class UpdateCurrency : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;

    private void OnEnable()
    {
        StatsManager.OnCoinChanged += UpdateValue;
    }

    private void Start()
    {
        UpdateValue(StatsManager.Instance.coins);
    }

    private void OnDisable()
    {
        StatsManager.OnCoinChanged -= UpdateValue;
    }

    private void UpdateValue(float value)
    {
        currencyText.text = value.ToString("N0");
    }
}
