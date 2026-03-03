using UnityEngine;
using UnityEngine.UI;


public class StallArea : MonoBehaviour
{
    public StallAreaData stallAreaData;

    public bool isUnlocked;
    public StallFoodData assignedFood;

    [Header("Visuals")]
    public GameObject lockOverlay;
    public GameObject emptyStallVisual; 
    public SpriteRenderer stallRenderer;


    private void Start()
    {
        LoadUnlockState();
        UpdateVisual();
    }


    private void OnMouseDown()
    {
        if (!isUnlocked)
        {
            StallUIManager.Instance.OpenUnlockPanel(this);
        }
        else if (assignedFood == null)
        {
            StallUIManager.Instance.OpenFoodSelection(this);
        }
        else
        {
            Debug.Log("Stall already selling " + assignedFood.stallFoodName);
        }
    }

    public void UnlockStall()
    {
        isUnlocked = true;
        SaveUnlockState();
        UpdateVisual();
    }

    public void AssignFood(StallFoodData food)
    {
        assignedFood = food;
        stallRenderer.sprite = food.stallFoodImage;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        lockOverlay.SetActive(!isUnlocked);
        emptyStallVisual.SetActive(isUnlocked && assignedFood == null);
    }

    
    void SaveUnlockState()
    {
        PlayerPrefs.SetInt(stallAreaData.stallID, 1);
    }

    void LoadUnlockState()
    {
        isUnlocked = PlayerPrefs.GetInt(stallAreaData.stallID, 0) == 1;
    }
    
}
