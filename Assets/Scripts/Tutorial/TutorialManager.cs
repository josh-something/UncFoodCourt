using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject currentPanel;

        public void OpenTutorialPanel(GameObject panel)
    {
        if (currentPanel != null && currentPanel != panel)
        {
            currentPanel.SetActive(false);
        }

        panel.SetActive(true);
        currentPanel = panel;
    }

    public void ClosePanel()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
            currentPanel = null;
        }
    }
}
