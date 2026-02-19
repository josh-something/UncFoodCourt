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

    public void UpdateValue(float value)
    {
        currencyText.text = string.Concat("$", value.ToString("N0"));
    }
}
