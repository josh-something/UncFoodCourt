using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public static event Action<float> OnCoinChanged;
    public static event Action<float> OnGoldBarsChanged;
    public static event Action<float,float> OnEnergyChanged;

    private int _energy;
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
            OnEnergyChanged?.Invoke(Energy, _maxEnergy);
        }
    }

    private int _maxEnergy = 10;

    
    
    
    public float coins;
    private float _oldCoins;
    public float goldBars;
    private float _oldGoldBars;

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
    
    private void Awake()
    {
        CreateSingleton();
    }

    public void AddOverflowEnergy(int amount)
    {
        Energy += amount;
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
