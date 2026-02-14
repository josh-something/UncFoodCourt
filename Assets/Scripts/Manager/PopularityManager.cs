using UnityEngine;
using UnityEngine.UI;

public class PopularityManager : MonoBehaviour
{
    [Range(0f, 100f)]
    public float populartiyPercentage = 0f;

    public Slider popularitySlider;

    public int PopulartiyStars()
    {
        int stars = Mathf.FloorToInt(populartiyPercentage / 20f);
        return Mathf.Clamp(stars, 0, 5);
    }

    public void AddPopularity(float Amount)
    {
        populartiyPercentage += Amount;
        populartiyPercentage = Mathf.Clamp(populartiyPercentage, 0f, 100f);

        Debug.Log($"Popularity increased by: {populartiyPercentage}%, (Stars = {PopulartiyStars()}");
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //popularity amount placeholderZ
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            AddPopularity(5);
        }

        //Slider
        populartiyPercentage = popularitySlider.value;
    }
}
