using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityValues : MonoBehaviour
{

    public int GetStoneMaxCapacity(int lvl)
    {
        int stoneMax = 0;
        //Debug.Log("WLVL:" + PlayerPrefs.GetInt("WarehouseLvl"));
        switch (lvl)
        {
            case 0:
                stoneMax = 100; break;
            case 1:
                stoneMax = 600; break;
            case 2:
                stoneMax = 1600; break;
            case 3:
                stoneMax = 3000; break;
            case 4:
                stoneMax = 10000; break;
        }

        return stoneMax;
    }

    public int GetSilverMaxCapacity(int lvl)
    {
        int silverMax = 0;
        switch (lvl)
        {
            case 0:
                silverMax = 0; break;
            case 1:
                silverMax = 350; break;
            case 2:
                silverMax = 900; break;
            case 3:
                silverMax = 1600; break;
            case 4:
                silverMax = 5000; break;
        }
        return silverMax;
    }
    
    public int GetGoldMaxCapacity(int lvl)
    {
        int goldMax = 0;
        switch (lvl)
        {
            case 0:
                goldMax = 0; break;
            case 1:
                goldMax = 0; break;
            case 2:
                goldMax = 0; break;
            case 3:
                goldMax = 900; break;
            case 4:
                goldMax = 2000; break;
        }
        return goldMax;
    }

    public int GetCitizensMaxCapacity(int lvl)
    {
        int citizensMax = 0;
        switch (lvl)
        {
            case 0:
                citizensMax = 10; break;
            case 1:
                citizensMax = 20; break;
            case 2:
                citizensMax = 40; break;
            case 3:
                citizensMax = 80; break;
            case 4:
                citizensMax = 160; break;
        }
        return citizensMax;
    }
}
