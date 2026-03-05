using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    [SerializeField] private StallArea[] stalls;

    private void Awake()
    {
        Instance = this;
    }

    // public bool CustomerBuyingChance()
    // {
    //     int stars = FindFirstObjectByType<PopularityManager>().PopularityStars();

    //     if (stars <= 2)
    //     {
    //         float chance = Random.value;

    //         if (chance <= 0.3f)
    //         {
    //             Debug.Log($"Customer did NOT buy!: Chance: {chance}");
    //             return false; // Customer did not buy
    //         }

    //         Debug.Log("Customer bought!");
    //         return true; // Customer bought
    //     }

    //     return true;
    // }

    // public void NewCustomerArrived(GameObject customerObject)
    // {
    //     bool willBuy = CustomerBuyingChance();

    //     if (willBuy)
    //     {
    //         customerObject.GetComponent<Customer>().TryBuyFood();
    //     }

    //     else
    //     {
    //         customerObject.GetComponent<Customer>().LeaveWithoutBuying();
    //     }
    // }

    public void ProcessNewCustomer(Customer customer) 
    {
        StallArea targetStall = GetRandomValidStall();

        if (targetStall == null)
        {
            customer.Initialize(false, null);
            return;
        }

        int stars = FindFirstObjectByType<PopularityManager>().PopularityStars();

        float buyChance;

        if (stars <= 2)
        {
            buyChance = 0.7f;   // 70% chance to buy (30% not buy)
        }
        else
        {
            buyChance = 1f;     // 100% buy
        }
        bool willBuy = Random.value < buyChance;
        Debug.Log($"Customer will buy: {willBuy} (Chance: {buyChance})");

        customer.Initialize(willBuy, targetStall);
    }

    private StallArea GetRandomValidStall()
    {
        List<StallArea> valid = new List<StallArea>();

        foreach (StallArea stall in stalls)
        {
            if (stall.isUnlocked && stall.HasFood())
            {
                valid.Add(stall);//add to list of valid stalls to buy from
                Debug.Log($"Valid stall found: {stall.name}");
            }
        }

        if (valid.Count == 0)
            return null;

        return valid[Random.Range(0, valid.Count)];
        
    }
}
