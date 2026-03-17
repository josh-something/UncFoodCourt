using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public int tutorialStep = 0;

    private void Awake()
    {
        Instance = this;
    }

    void NextStep()
    {
        tutorialStep++;
        Debug.Log($"Tutorial Step: {tutorialStep}");
    }

    public bool IsStep(int step)
    {
        return tutorialStep == step;
    }
}
