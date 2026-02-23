using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    public int Energy
    {
        get { return Energy; }
        set
        {
            if (value >= maxEnergy)
            {
                Energy = maxEnergy;
            }
            else
            {
                Energy = value;
            }
        }
    }
    private int maxEnergy = 10;
    public float coins;
    public float goldBars;

    public void AddOverflowEnergy(int amount)
    {
        Energy += amount;
    }

    private void Awake()
    {
        CreateSingleton();
    }
    

    void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
