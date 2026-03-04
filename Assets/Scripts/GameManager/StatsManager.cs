using System;
using UnityEngine;
using System.Collections.Generic;


public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    //EVENTS
    public static event Action<float> OnCoinChanged;
    public static event Action<float> OnGoldBarsChanged;
    public static event Action<float,float> OnEnergyChanged;

    //CURRENCY
    [SerializeField] private float coins;
    [SerializeField] private float goldBars;
    private float _previousCoins;
    private float _previousGoldBars;

    // ENERGY
    [SerializeField] private int energy;
    [SerializeField] private int maxEnergy = 10;

    
    public HashSet<StallFoodData> purchasedFoods = new HashSet<StallFoodData>();

     private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        _previousCoins = coins;
        _previousGoldBars = goldBars;

        OnCoinChanged?.Invoke(coins);
        OnGoldBarsChanged?.Invoke(goldBars);
        OnEnergyChanged?.Invoke(energy, maxEnergy);
    }

    private void Update()
    {
        if (!Mathf.Approximately(_previousCoins, coins))
        {
            OnCoinChanged?.Invoke(coins);
            _previousCoins = coins;
        }

        if (!Mathf.Approximately(_previousGoldBars, goldBars))
        {
            OnGoldBarsChanged?.Invoke(goldBars);
            _previousGoldBars = goldBars;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AddCoins(10000);
        }
    }


    public void AddCoins(float amount)
    {
        coins += amount;
        OnCoinChanged?.Invoke(coins);
    }

    public bool TrySpendCoins(float amount)
    {
        if (coins < amount)
            return false;

        coins -= amount;
        OnCoinChanged?.Invoke(coins);
        return true;
    }

    public float GetCoins() => coins;


    public void AddGold(float amount)
    {
        goldBars += amount;
        OnGoldBarsChanged?.Invoke(goldBars);
    }

    public bool TrySpendGold(float amount)
    {
        if (goldBars < amount)
            return false;

        goldBars -= amount;
        OnGoldBarsChanged?.Invoke(goldBars);
        return true;
    }

    public float GetGold() => goldBars;


    public int Energy
    {
        get => energy;
        set
        {
            energy = Mathf.Clamp(value, 0, maxEnergy);
            OnEnergyChanged?.Invoke(energy, maxEnergy);
        }
    }

    public void AddEnergy(int amount)
    {
        Energy += amount;
    }

    public void SpendEnergy(int amount)
    {
        Energy -= amount;
    }

    public int GetMaxEnergy() => maxEnergy;

    public bool IsFoodPurchased(StallFoodData food)
    {
        return purchasedFoods.Contains(food);
    }

    public void MarkFoodAsPurchased(StallFoodData food)
    {
        purchasedFoods.Add(food);
    }
}
