using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Resources")]
    /*[SerializeField]*/ private int goldAmount;
    /*[SerializeField]*/ private int silverAmount;
    /*[SerializeField]*/ private int stoneAmount;
    /*[SerializeField]*/ private int citizensAmount;

    [Header("Structures Game Objects")]
    [SerializeField] private GameObject warehouse_GO;
    [SerializeField] private GameObject sheriff_GO;
    [SerializeField] private GameObject saloon_GO;
    [SerializeField] private GameObject stable_GO;
    [SerializeField] private GameObject home_GO;
    [SerializeField] private GameObject mine_GO;

    [Header("Others")]
    private CapacityValues capacityValues;
    private ScenesController scenesController;
    //private BuildingController buildingController;
    [SerializeField] private GameObject textForNoMoreCapacity;

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

        //para testar --------------
        //AddStone(1000);
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddStone(90);
            AddSilver(90);
            AddGold(90);
            //Debug.Log("Sheriff Level: " + PlayerPrefs.GetInt("SheriffLvl"));
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            goldAmount = 0;
            silverAmount = 0;
            stoneAmount = 0;
            citizensAmount = 10;
            
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
        saveGame();
    }

    public void saveGame(){
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
        PlayerPrefs.SetInt("CitizensAmount", 10);

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
    public int AddGold(int amount)
    {
        if(capacityValues.GetGoldMaxCapacity(getWarehouseLevel()) >= (GetGoldAmount() + amount)){
            goldAmount += amount;
            saveGame();
            return 0;
        }else {
            //aviso
            StartCoroutine(ShowAndHide(textForNoMoreCapacity, 2.0f));
            Debug.Log("Full of gold");
            goldAmount = capacityValues.GetGoldMaxCapacity(getWarehouseLevel());
            saveGame();
            return (GetGoldAmount() + amount) - capacityValues.GetGoldMaxCapacity(getWarehouseLevel()); //excesso
        }
    }
    public bool RemoveGold(int amount)
    {
        if((GetGoldAmount() - amount) >= 0){
            goldAmount -= amount;
            saveGame();
            return true;
        } else {
            //aviso
            Debug.Log("not enough gold");
            saveGame();
            return false;
        }
    }

    public int GetSilverAmount()
    {
        return silverAmount;
    }
    public int AddSilver(int amount)
    {
        if(capacityValues.GetSilverMaxCapacity(getWarehouseLevel()) >= (GetSilverAmount() + amount)){
            silverAmount += amount;
            saveGame();
            return 0;
        } else {
            //aviso
            StartCoroutine(ShowAndHide(textForNoMoreCapacity, 2.0f));
            silverAmount = capacityValues.GetSilverMaxCapacity(getWarehouseLevel());
            Debug.Log("Full of silver");
            saveGame();
            return (GetSilverAmount() + amount) - capacityValues.GetSilverMaxCapacity(getWarehouseLevel()); //excesso
        }
    }
    public bool RemoveSilver(int amount)
    {
        if((GetSilverAmount() - amount) >= 0){
            silverAmount -= amount;
            saveGame();
            return true;
        } else {
            //aviso
            Debug.Log("not enough silver");
            saveGame();
            return false;
        }
    }

    public int GetStoneAmount()
    {
        return stoneAmount;
    }
    public int AddStone(int amount)
    {
        if(capacityValues.GetStoneMaxCapacity(getWarehouseLevel()) >= (GetStoneAmount() + amount)){
            stoneAmount += amount;
            saveGame();
            return 0;
        } else {
            //aviso
            StartCoroutine(ShowAndHide(textForNoMoreCapacity, 2.0f));
            stoneAmount = capacityValues.GetStoneMaxCapacity(getWarehouseLevel());
            Debug.Log("Full of stone");
            saveGame();
            return (GetStoneAmount() + amount) - capacityValues.GetStoneMaxCapacity(getWarehouseLevel()); //excesso
        }
    }
    public bool RemoveStone(int amount)
    {
        if((GetStoneAmount() - amount) >= 0){
            stoneAmount -= amount;
            saveGame();
            return true;
        } else {
            //aviso
            Debug.Log("not enough stone");
            saveGame();
            return false;
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
        //Debug.Log(PlayerPrefs.GetInt("WarehouseLvl"));
        return PlayerPrefs.GetInt("WarehouseLvl");
    }

    public int getHomeLevel(){
        //return home_GO.GetComponent<BuildingController>().GetLevel();
        //Debug.Log(PlayerPrefs.GetInt("HomeLvl"));
        return PlayerPrefs.GetInt("HomeLvl");
    }

    IEnumerator ShowAndHide(GameObject go, float delay){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    public void SetLevel(string key, int level)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, level);
        }
    }
}
