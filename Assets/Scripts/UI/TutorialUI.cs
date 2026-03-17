using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TMP_Text tutorialText;

    private void Update()
    {
        int step = TutorialManager.Instance.tutorialStep;

        switch (step)
        {
            case 0:
                tutorialText.text = "Welcome! ";
                break;
            case 1:
                tutorialText.text = "Step 1: You need to Unlock a stall first.";
                break;
            case 2:
                tutorialText.text = "Step 2: Press the empty stall to choose the food you ar";
                break;
            case 3:
                tutorialText.text = "Step 3:";
                break;
        }
    }
}
