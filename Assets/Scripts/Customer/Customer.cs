using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool willBuy;
    private StallArea targetStall;

    public void Initialize(bool buyingDecision, StallArea stall)
    {
        willBuy = buyingDecision;
        targetStall = stall;

        if (willBuy)
        {
            TryBuyFood();
        }
        else
        {
            LeaveWithoutBuying();
        }
    }

    public void TryBuyFood()
    {
        if (targetStall == null || !targetStall.HasFood())
        {
            LeaveWithoutBuying();
            return;
        }

        FoodStallUpgrades upgrades = targetStall.GetComponent<FoodStallUpgrades>();

        if (upgrades == null)
        {
            LeaveWithoutBuying();
            return;
        }

        bool success = upgrades.TryProcessOrder();

        if (!success)
        {
            LeaveWithoutBuying();
            return;
        }
        Debug.Log("Customer bought from " + targetStall.name);

        Destroy(gameObject, 2f);
    }

    public void LeaveWithoutBuying()
    {
        // Logic for leaving without buying
        Debug.Log("Customer left without buying.");
        
        Destroy(gameObject, 2f); // Destroy the customer object after a short delay
    }
}
