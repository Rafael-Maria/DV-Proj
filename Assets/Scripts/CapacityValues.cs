using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityValues : MonoBehaviour
{
    public int GetStoneMaxCapacity()
    {
        int stoneMax = 0;
        Debug.Log("WLVL:" + PlayerPrefs.GetInt("WarehouseLvl"));
        switch (PlayerPrefs.GetInt("WarehouseLvl")) //MUDAR OS VALORES DE CAPACIDADE PEDRA CONSOANTE O NIVEL DO WAREHOUES
        {
            case 0:
                stoneMax = 9000; break;
            case 1:
                stoneMax = 200; break;
            case 2:
                stoneMax = 300; break;
        }

        return stoneMax;
    }

    public int GetSilverMaxCapacity()
    {
        int silverMax = 0;
        switch (PlayerPrefs.GetInt("WarehouseLvl")) //MUDAR OS VALORES DE CAPACIDADE PEDRA CONSOANTE O NIVEL DO WAREHOUES
        {
            case 0:
                silverMax = 100; break;
            case 1:
                silverMax = 200; break;
            case 2:
                silverMax = 300; break;
        }
        return silverMax;
    }
    
    public int GetGoldMaxCapacity()
    {
        int goldMax = 0;
        switch (PlayerPrefs.GetInt("WarehouseLvl")) //MUDAR OS VALORES DE CAPACIDADE PEDRA CONSOANTE O NIVEL DO WAREHOUES
        {
            case 0:
                goldMax = 100; break;
            case 1:
                goldMax = 200; break;
            case 2:
                goldMax = 300; break;
        }
        return goldMax;
    }
}
