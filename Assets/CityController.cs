using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour, ISaveable
{
    public string cityName;
    public int cityLevel;
    public int maxPopulation;
    public int maxStructures;
    public int coins;

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        cityName = saveData.cityName;
        cityLevel = saveData.cityLevel;
        maxPopulation = saveData.maxPopulation;
        maxStructures = saveData.maxStructures;
        coins = saveData.coins;
    }

    public object SaveState()
    {
        return new SaveData()
        {
            cityName = this.cityName,
            cityLevel = this.cityLevel,
            maxPopulation = this.maxPopulation,
            maxStructures = this.maxStructures,
            coins = this.coins
        };
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    private struct SaveData
    {
        public string cityName;
        public int cityLevel;
        public int maxPopulation;
        public int maxStructures;
        public int coins;
    }
}
