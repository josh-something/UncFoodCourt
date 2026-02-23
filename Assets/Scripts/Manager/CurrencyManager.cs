using JetBrains.Annotations;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int startingCoins = 30000;
    public int currentCoins;



    void Start()
    {
        currentCoins = startingCoins;
    }

    void Update()
    {
        
    }

    public void AddCoins(float amount)
    {
        // if customer buys food
    }

    public void ReduceCoins()
    {
        //If bought stalls/from shop
        //upgrades
    }


}
