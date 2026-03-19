using Unity.VisualScripting;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }

    [Header("Minigame Panels")]
    public GameObject burgerMinigamePanel;
    public GameObject pizzaMinigamePanel;
    public GameObject sundaeMinigamePanel;  

    public GameObject MinigameObject; // The parent object that contains all minigame panels, used to toggle visibility

    public GameObject gameFinishedPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenMinigame(MinigameType type)
    {
        
        switch (type)
        {
            case MinigameType.Burger:
                MinigameObject.SetActive(true);
                UIManager.Instance.OpenMinigamePanel(burgerMinigamePanel);
                break;
            case MinigameType.Pizza:
                MinigameObject.SetActive(true);
                UIManager.Instance.OpenMinigamePanel(pizzaMinigamePanel);
                break;
            case MinigameType.Sundae:
                MinigameObject.SetActive(true);
                UIManager.Instance.OpenMinigamePanel(sundaeMinigamePanel);
                break;
        }
    }

    public void CloseFinishedGame()
    {
        gameFinishedPanel.SetActive(false);

        StallArea stall = StallUIManager.Instance.GetCurrentStall();

        if (stall != null && stall.assignedFood != null)
        {
            FoodStallUpgrades upgrades = stall.GetComponent<FoodStallUpgrades>();

            if (upgrades != null)
            {
                upgrades.currentStock += 3;

                int maxStock = stall.assignedFood.maxStock;
                upgrades.currentStock = Mathf.Min(upgrades.currentStock, maxStock);
            }
        }

        Time.timeScale = 1f;
        BurgerManager.instance.ResetMinigame();
        UIManager.Instance.CloseMinigamePanel();
    }

    public void EndMinigame()
    {
        Time.timeScale = 0f;
        gameFinishedPanel.SetActive(true);
    }


}
