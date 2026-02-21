using System;
using TMPro;
using UnityEngine;

public class UpdateEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text energyText;
    void OnEnable()
    {
        StatsManager.OnEnergyChanged += UpdateText;
    }

    void OnDisable()
    {
        StatsManager.OnEnergyChanged -= UpdateText;
    }

    private void Start()
    {
        energyText = GetComponent<TMP_Text>();
        UpdateText(StatsManager.Instance.Energy, 10);
    }

    private void UpdateText(float current, float max)
    {
        energyText.text = current + "/" + max;
    }
}
