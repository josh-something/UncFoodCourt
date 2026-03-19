using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only ingredients
        if (other.CompareTag("Ingredient"))
        {
            // Get the IngredientMovement component
            IngredientMovement ingredient = other.GetComponent<IngredientMovement>();
            if (ingredient != null)
            {
                FindObjectOfType<BurgerMiniGame>().IngredientFell(ingredient);
            }

            Destroy(other.gameObject); // remove from scene
        }
    }
}