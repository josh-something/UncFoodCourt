using UnityEngine;
using UnityEngine.UI;

public class PopularityManager : MonoBehaviour
{
    [Range(0f, 100f)]
    public float popularityPercentage = 0f;

    //public Slider popularitySlider;

    public int PopularityStars()
    {
        int stars = Mathf.FloorToInt(popularityPercentage / 20f);
        return Mathf.Clamp(stars, 0, 5);
    }

    public void AddPopularity(float Amount)
    {
        popularityPercentage += Amount;
        Debug.Log("Amount received: " + Amount);
        popularityPercentage = Mathf.Clamp(popularityPercentage, 0f, 100f);

        Debug.Log($"Popularity increased by: {popularityPercentage}%, (Stars = {PopularityStars()})");
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //popularity amount placeholder
        if (Input.GetKeyUp(KeyCode.Q))
        {
            AddPopularity(5);
        }

        //Slider
        //popularityPercentage = popularitySlider.value;
    }
}
