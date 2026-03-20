using UnityEngine;
using TMPro;

public class BurgerMiniGame : MonoBehaviour
{
    [Header("Ingredients")]
    public GameObject[] ingredientPrefabs;
    public Transform spawnPoint;

    private int nextIngredientIndex = 0;
    private GameObject currentIngredient;

    private int stackedIngredients = 0;
    private int ingredientsFallen = 0;

    [Header("UI")]
    public GameObject endGamePanel;
    public TMP_Text finalScoreText;

    private bool canSpawnNext = true; // controls spawning

    void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (!canSpawnNext) return;

        if (nextIngredientIndex >= 6)
        {
            EndGame();
            return;
        }

        currentIngredient = Instantiate(
            ingredientPrefabs[nextIngredientIndex],
            spawnPoint.position,
            Quaternion.identity,
            transform
        );

        nextIngredientIndex++;
        canSpawnNext = false; // wait until ingredient lands or falls
    }

    // Called when ingredient successfully lands
    public void IngredientLanded(IngredientMovement ingredient)
    {
        stackedIngredients++;
        canSpawnNext = true;
        SpawnNext();
    }

    // Called when ingredient falls
    public void IngredientFell(IngredientMovement ingredient)
    {
        ingredientsFallen++;
        canSpawnNext = true;
        SpawnNext();
    }

    int CalculateScore()
    {
        return Mathf.Max(stackedIngredients - ingredientsFallen, 0);
    }

    void EndGame()
    {
        int finalScore = CalculateScore();

        if (endGamePanel != null) endGamePanel.SetActive(true);
        if (finalScoreText != null) finalScoreText.text = "Score: " + finalScore;

        Debug.Log("Final Score: " + finalScore);
        MinigameManager.Instance.EndMinigame();

        //Time.timeScale = 0f; // pause game
    }
}