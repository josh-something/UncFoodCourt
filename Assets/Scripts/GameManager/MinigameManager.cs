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
                UIManager.Instance.OpenPanel(burgerMinigamePanel);
                break;
            case MinigameType.Pizza:
                MinigameObject.SetActive(true);
                UIManager.Instance.OpenPanel(pizzaMinigamePanel);
                break;
            case MinigameType.Sundae:
                MinigameObject.SetActive(true);
                UIManager.Instance.OpenPanel(sundaeMinigamePanel);
                break;
        }
    }

    // public void StartBurgerMinigame()
    // {
    //     UIManager.Instance.OpenPanel(burgerMinigamePanel);

    // }

    // public void StartPizzaMinigame()
    // {
    //     UIManager.Instance.OpenPanel(pizzaMinigamePanel);
    // }

    // public void StartSundaeMinigame()
    // {
    //     UIManager.Instance.OpenPanel(sundaeMinigamePanel);
    // }


}
