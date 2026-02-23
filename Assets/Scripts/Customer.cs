using UnityEngine;

public class Customer : MonoBehaviour
{

    private bool willBuy;

    public void Initialize(bool buyingDecision)
    {
        willBuy = buyingDecision;

        if (willBuy)
        {
            BuyFood();
        }
        else
        {
            LeaveWithoutBuying();
        }
    }

    public void BuyFood()
    {
        // Logic for buying food
        Debug.Log("Customer bought food!");
        // Add coins to currency manager, update stats, etc.
    }

    public void LeaveWithoutBuying()
    {
        // Logic for leaving without buying
        Debug.Log("Customer left without buying.");
        // Update stats, etc.
        Destroy(gameObject, 2f); // Destroy the customer object after a short delay
    }
}
