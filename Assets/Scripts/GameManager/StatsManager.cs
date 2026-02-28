using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public static event Action<float> OnCoinChanged;
    public static event Action<float> OnGoldBarsChanged;
    public static event Action<float,float> OnEnergyChanged;

    [SerializeField] private int _energy;

    private int _maxEnergy = 10;

    
    
    
    public float coins;
    public float goldBars;


    private float _oldCoins;
    private float _oldGoldBars;



    public int Energy
    {
        get => _energy;
        set
        {
            if (value >= _maxEnergy)
            {
                _energy = _maxEnergy;
            }
            else
            {
                _energy = value;
            }
            OnEnergyChanged?.Invoke(_energy, _maxEnergy);
        }
    }



    private void Start()
    {
        _oldCoins = coins;
        _oldGoldBars = goldBars;
    }

    private void Update()
    {
        if (!Mathf.Approximately(_oldCoins, coins))
        {
            OnCoinChanged?.Invoke(coins);
            _oldCoins = coins;
        }

        if (!Mathf.Approximately(_oldGoldBars, goldBars))
        {
            OnGoldBarsChanged?.Invoke(goldBars);
            _oldGoldBars = goldBars;
        }
    }


    public bool TrySpendCoins(float amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }

        return false;
    }
    
    private void Awake()
    {
        CreateSingleton();
    }

    public void AddOverflowEnergy(int amount)
    {
        _energy += amount;
        OnEnergyChanged?.Invoke(_energy, _maxEnergy);
    }

    private void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
