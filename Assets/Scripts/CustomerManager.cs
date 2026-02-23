using UnityEngine;

public class CustomerManager : MonoBehaviour
{

    public static CustomerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool CustomerBuyingChance()
    {
        int stars = FindFirstObjectByType<PopularityManager>().PopularityStars();

        if (stars <= 2)
        {
            float chance = Random.value;

            if (chance <= 0.3f)
            {
                Debug.Log($"Customer did NOT buy!: Chance: {chance}");
                return false; // Customer did not buy
            }

            Debug.Log("Customer bought!");
            return true; // Customer bought
        }

        return true;
    }

    public void NewCustomerArrived(GameObject customerObject)
    {
        bool willBuy = CustomerBuyingChance();

        if (willBuy)
        {
            customerObject.GetComponent<Customer>().BuyFood();
        }

        else
        {
            customerObject.GetComponent<Customer>().LeaveWithoutBuying();
        }
    }
}
