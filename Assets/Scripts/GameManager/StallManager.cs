using System.Collections.Generic;
using UnityEngine;

public class StallManager : MonoBehaviour
{
    public List<Stalls> stalls;

    public void AddStall(StallDisplayPanelInfo info)
    {
        bool foundStall = false;
        foreach (Stalls stall in stalls)
        {
            if (stall.isEmpty)
            {
                foundStall = true;
                stall.stalltype = info;
                stall.isEmpty = false;
                stall.UpdateStall();
                break;
            }
        }

        if (!foundStall)
        {
            Debug.Log("Stall not found");
        }
    }
}
