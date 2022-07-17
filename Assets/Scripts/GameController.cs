using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private ScenesController scenesController;
    //private BuildingController buildingController;

    void Start()
    {
        //buildingController = GameObject.FindGameObjectWithTag("buildingController").GetComponent<BuildingController>();
        capacityValues = GameObject.FindGameObjectWithTag("gameController").gameObject.GetComponent<CapacityValues>();
        scenesController = GameObject.FindGameObjectWithTag("loading").gameObject.GetComponent<ScenesController>();

        if (PlayerPrefs.HasKey("GoldAmount"))
            goldAmount = PlayerPrefs.GetInt("GoldAmount");
        if (PlayerPrefs.HasKey("SilverAmount"))
            silverAmount = PlayerPrefs.GetInt("SilverAmount");
        if (PlayerPrefs.HasKey("StoneAmount"))
            stoneAmount = PlayerPrefs.GetInt("StoneAmount");
        if (PlayerPrefs.HasKey("CitizensAmount"))
            citizensAmount = PlayerPrefs.GetInt("CitizensAmount");

        if (PlayerPrefs.HasKey("WarehouseLvl") && warehouse_GO)
            warehouse_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("WarehouseLvl"));

        if (PlayerPrefs.HasKey("SheriffLvl") && sheriff_GO)
           sheriff_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("SheriffLvl"));

        if (PlayerPrefs.HasKey("SaloonLvl") && saloon_GO)
            saloon_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("SaloonLvl"));

        if (PlayerPrefs.HasKey("StableLvl") && stable_GO)
            stable_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("StableLvl"));

        if (PlayerPrefs.HasKey("HomeLvl") && home_GO)
            home_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("HomeLvl"));

        if (PlayerPrefs.HasKey("MineLvl") && mine_GO)
            mine_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("MineLvl"));

        AddStone(100);
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            goldAmount = 0;
            silverAmount = 0;
            stoneAmount = 0;
            citizensAmount = 0;
            
            if (warehouse_GO)
                warehouse_GO.GetComponent<BuildingController>().Reset();
            if (sheriff_GO)
                sheriff_GO.GetComponent<BuildingController>().Reset();
            if (saloon_GO)
                saloon_GO.GetComponent<BuildingController>().Reset();
            if (stable_GO)
                stable_GO.GetComponent<BuildingController>().Reset();
            if (home_GO)
                home_GO.GetComponent<BuildingController>().Reset();
            if (mine_GO)
                mine_GO.GetComponent<BuildingController>().Reset();


            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs reseted");
            initiatePlayerPrefs();

            if(scenesController != null)
                scenesController.loadCity();
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("GoldAmount", goldAmount);
        PlayerPrefs.SetInt("SilverAmount", silverAmount);
        PlayerPrefs.SetInt("StoneAmount", stoneAmount);
        PlayerPrefs.SetInt("CitizensAmount", citizensAmount);

        if (warehouse_GO)
            PlayerPrefs.SetInt("WarehouseLvl", warehouse_GO.GetComponent<BuildingController>().GetLevel());
        if (sheriff_GO)
            PlayerPrefs.SetInt("SheriffLvl", sheriff_GO.GetComponent<BuildingController>().GetLevel());
        if (saloon_GO)
            PlayerPrefs.SetInt("SaloonLvl", saloon_GO.GetComponent<BuildingController>().GetLevel());
        if (stable_GO)
            PlayerPrefs.SetInt("StableLvl", stable_GO.GetComponent<BuildingController>().GetLevel());
        if (home_GO)
            PlayerPrefs.SetInt("HomeLvl", home_GO.GetComponent<BuildingController>().GetLevel());
        if (mine_GO)
            PlayerPrefs.SetInt("MineLvl", mine_GO.GetComponent<BuildingController>().GetLevel());

        PlayerPrefs.Save();
    }

    private void initiatePlayerPrefs(){
        PlayerPrefs.SetInt("GoldAmount", 0);
        PlayerPrefs.SetInt("SilverAmount", 0);
        PlayerPrefs.SetInt("StoneAmount", 0);
        PlayerPrefs.SetInt("CitizensAmount", 0);

        PlayerPrefs.SetInt("WarehouseLvl", 0);
        PlayerPrefs.SetInt("SheriffLvl", 0);
        PlayerPrefs.SetInt("SaloonLvl", 0);
        PlayerPrefs.SetInt("StableLvl", 0);
        PlayerPrefs.SetInt("HomeLvl", 0);
        PlayerPrefs.SetInt("MineLvl", 0);
        PlayerPrefs.Save();
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
    public void AddGold(int amount)
    {
        if(capacityValues.GetGoldMaxCapacity(getWarehouseLevel()) >= (GetGoldAmount() + amount)){
            goldAmount += amount;
        }
    }
    public int GetSilverAmount()
    {
        return silverAmount;
    }
    public void AddSilver(int amount)
    {
        if(capacityValues.GetSilverMaxCapacity(getWarehouseLevel()) >= (GetSilverAmount() + amount)){
            silverAmount += amount;
        }
    }
    public int GetStoneAmount()
    {
        return stoneAmount;
    }
    public void AddStone(int amount)
    {
        if(capacityValues.GetStoneMaxCapacity(getWarehouseLevel()) >= (GetStoneAmount() + amount)){
            stoneAmount += amount;
        } else {
            //aviso
            Debug.Log("TA CHEIO");
        }
    }
    public int GetCitizensAmount()
    {
        return citizensAmount;
    }
    public void AddCitizens(int amount)
    {
        if(capacityValues.GetCitizensMaxCapacity(getHomeLevel()) >= (GetCitizensAmount() + amount)){
            citizensAmount += amount;
        }
    }

    public int getWarehouseLevel(){
        //return warehouse_GO.GetComponent<BuildingController>().GetLevel();
        Debug.Log(PlayerPrefs.GetInt("WarehouseLvl"));
        return PlayerPrefs.GetInt("WarehouseLvl");
    }

    public int getHomeLevel(){
        //return home_GO.GetComponent<BuildingController>().GetLevel();
        Debug.Log(PlayerPrefs.GetInt("HomeLvl"));
        return PlayerPrefs.GetInt("HomeLvl");
    }
}
