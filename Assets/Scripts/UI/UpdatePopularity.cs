using UnityEngine;
using UnityEngine.UI;

public class UpdatePopularity : MonoBehaviour
{
    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        PopularityManager.OnPopularityChanged += UpdateSlider;
    }

    void OnDisable()
    {
        PopularityManager.OnPopularityChanged -= UpdateSlider;
    }

    private void UpdateSlider(float value)
    {
        var percentage = value / 100f;
        _slider.value = percentage;
    }
}
