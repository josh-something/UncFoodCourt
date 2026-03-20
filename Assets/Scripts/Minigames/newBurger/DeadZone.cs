using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [Header("Reference to the MiniGame")]
    public BurgerMiniGame burgerMiniGame; // assign in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Ingredient")) return;

        IngredientMovement ingredient = other.GetComponent<IngredientMovement>();
        if (ingredient != null && burgerMiniGame != null)
        {
            burgerMiniGame.IngredientFell(ingredient);
        }


        Destroy(other.gameObject);
    }
}