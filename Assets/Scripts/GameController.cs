using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private int goldAmount;
    [SerializeField] private int silverAmount;
    [SerializeField] private int stoneAmount;
    [SerializeField] private int citizensAmount;

    [Header("Structures Game Objects")]
    [SerializeField] private GameObject warehouse_GO;
    [SerializeField] private GameObject sheriff_GO;
    [SerializeField] private GameObject saloon_GO;
    [SerializeField] private GameObject stable_GO;
    [SerializeField] private GameObject home_GO;
    [SerializeField] private GameObject mine_GO;

    private CapacityValues capacityValues;
    private BuildingController buildingController;

    void Start()
    {
        //buildingController = GameObject.FindGameObjectWithTag("buildingController").GetComponent<BuildingController>();
        capacityValues = GameObject.FindGameObjectWithTag("gameController").gameObject.GetComponent<CapacityValues>();


        if (PlayerPrefs.HasKey("GoldAmount"))
            goldAmount = PlayerPrefs.GetInt("GoldAmount");
        if (PlayerPrefs.HasKey("SilverAmount"))
            silverAmount = PlayerPrefs.GetInt("SilverAmount");
        if (PlayerPrefs.HasKey("StoneAmount"))
            stoneAmount = PlayerPrefs.GetInt("StoneAmount");
        if (PlayerPrefs.HasKey("CitizensAmount"))
            citizensAmount = PlayerPrefs.GetInt("CitizensAmount");

        if (PlayerPrefs.HasKey("WarehouseLvl"))
            warehouse_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("WarehouseLvl"));
        if (PlayerPrefs.HasKey("SheriffLvl"))
            sheriff_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("SheriffLvl"));
        if (PlayerPrefs.HasKey("SaloonLvl"))
            saloon_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("SaloonLvl"));
        if (PlayerPrefs.HasKey("StableLvl"))
            stable_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("StableLvl"));
        if (PlayerPrefs.HasKey("HomeLvl"))
            home_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Home"));
        if (PlayerPrefs.HasKey("MineLvl"))
            mine_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("MineLvl"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            goldAmount = 0;
            silverAmount = 0;
            stoneAmount = 0;
            citizensAmount = 0;
            warehouse_GO.GetComponent<BuildingController>().Reset();
            sheriff_GO.GetComponent<BuildingController>().Reset();
            saloon_GO.GetComponent<BuildingController>().Reset();
            stable_GO.GetComponent<BuildingController>().Reset();
            home_GO.GetComponent<BuildingController>().Reset();
            mine_GO.GetComponent<BuildingController>().Reset();
            PlayerPrefs.DeleteAll();
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("GoldAmount", goldAmount);
        PlayerPrefs.SetInt("SilverAmount", silverAmount);
        PlayerPrefs.SetInt("StoneAmount", stoneAmount);
        PlayerPrefs.SetInt("CitizensAmount", citizensAmount);

        if (warehouse_GO != null)
            PlayerPrefs.SetInt("WarehouseLvl", warehouse_GO.GetComponent<BuildingController>().GetLevel());
        if (sheriff_GO != null)
            PlayerPrefs.SetInt("SheriffLvl", sheriff_GO.GetComponent<BuildingController>().GetLevel());
        if (saloon_GO != null)
            PlayerPrefs.SetInt("SaloonLvl", saloon_GO.GetComponent<BuildingController>().GetLevel());
        if (stable_GO != null)
            PlayerPrefs.SetInt("StableLvl", stable_GO.GetComponent<BuildingController>().GetLevel());
        if (home_GO != null)
            PlayerPrefs.SetInt("HomeLvl", home_GO.GetComponent<BuildingController>().GetLevel());
        if (mine_GO != null)
            PlayerPrefs.SetInt("MineLvl", mine_GO.GetComponent<BuildingController>().GetLevel());
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
    public void AddGold(int amount)
    {
        if(capacityValues.GetGoldMaxCapacity() >= (GetGoldAmount() + amount)){
            goldAmount += amount;
        }
    }
    public int GetSilverAmount()
    {
        return silverAmount;
    }
    public void AddSilver(int amount)
    {
        if(capacityValues.GetSilverMaxCapacity() >= (GetSilverAmount() + amount)){
            silverAmount += amount;
        }
    }
    public int GetStoneAmount()
    {
        return stoneAmount;
    }
    public void AddStone(int amount)
    {
        if(capacityValues.GetStoneMaxCapacity() >= (GetStoneAmount() + amount)){
            stoneAmount += amount;
        }
    }
    public int GetCitizensAmount()
    {
        return citizensAmount;
    }
    public void AddCitizens(int amount)
    {
        if(capacityValues.GetCitizensMaxCapacity() >= (GetCitizensAmount() + amount)){
            citizensAmount += amount;
        }
    }
}
