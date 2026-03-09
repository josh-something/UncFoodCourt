using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject BackgroundOverlay;
    [SerializeField] private GameObject MinigameOverlay;

    private GameObject currentOpenPanel;

    public static bool InputLocked;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenBackgroundOverlay()
    {
        BackgroundOverlay.SetActive(true);
    }

    public void CloseBackgroundOverlay()
    {
        BackgroundOverlay.SetActive(false);
    }

    public void OpenPanel(GameObject panel) // Call this to open any panel, it will automatically close the currently open one
    {
        if (currentOpenPanel != null && currentOpenPanel != panel) // Close current panel if it's different from the one being opened
        {
            currentOpenPanel.SetActive(false);
        }
        currentOpenPanel = panel;
        panel.SetActive(true);
        Time.timeScale = 0f;
        BackgroundOverlay.SetActive(true);
        InputLocked = true;
    }

    public void CloseCurrentPanel() // Call this to close whatever panel is currently open
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
        Time.timeScale = 1f;
        BackgroundOverlay.SetActive(false);
        InputLocked = false;
    }

    public void OpenMinigamePanel(GameObject panel)
    {

        if (currentOpenPanel != null && currentOpenPanel != panel) // Close current panel if it's different from the one being opened
        {
            currentOpenPanel.SetActive(false);
        }
        currentOpenPanel = panel;
        panel.SetActive(true);
        Time.timeScale = 0f;
        MinigameOverlay.SetActive(true);
        InputLocked = true;
    }

    public void CloseMinigamePanel() // Call this to close whatever panel is currently open
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
        Time.timeScale = 1f;
        MinigameOverlay.SetActive(false);
        InputLocked = false;
    } 
}
