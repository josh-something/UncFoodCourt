using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }

    [Header("Minigame Panels")]
    public GameObject pizzaMinigamePanel;
    public GameObject sundaeMinigamePanel;
    public GameObject PanelBackgroundOverlay;

    [Header("Minigame Prefabs")]
    public GameObject burgerMinigamePrefab;
    private GameObject burgerMinigameInstance; // To keep track of the instantiated burger minigame





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
                burgerMinigameInstance = Instantiate(burgerMinigamePrefab, MinigameObject.transform);
                UIManager.Instance.OpenMinigamePanel(PanelBackgroundOverlay);

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
        Destroy(burgerMinigameInstance); // Destroy the burger minigame instance
        UIManager.Instance.CloseMinigamePanel();
    }

    public void EndMinigame()
    {
        //Time.timeScale = 0f;
        gameFinishedPanel.SetActive(true);
    }


}
