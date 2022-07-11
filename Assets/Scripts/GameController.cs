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

    void Start()
    {
        if (PlayerPrefs.HasKey("Gold"))
            goldAmount = PlayerPrefs.GetInt("Gold");
        if (PlayerPrefs.HasKey("Silver"))
            silverAmount = PlayerPrefs.GetInt("Silver");
        if (PlayerPrefs.HasKey("Stone"))
            stoneAmount = PlayerPrefs.GetInt("Stone");
        if (PlayerPrefs.HasKey("Citizens"))
            citizensAmount = PlayerPrefs.GetInt("Citizens");

        if (PlayerPrefs.HasKey("Warehouse"))
            warehouse_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Warehouse"));
        if (PlayerPrefs.HasKey("Sheriff"))
            sheriff_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Sheriff"));
        if (PlayerPrefs.HasKey("Saloon"))
            saloon_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Saloon"));
        if (PlayerPrefs.HasKey("Stable"))
            stable_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Stable"));
        if (PlayerPrefs.HasKey("Home"))
            home_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Home"));
        if (PlayerPrefs.HasKey("Mine"))
            mine_GO.GetComponent<BuildingController>().SetLevel(PlayerPrefs.GetInt("Mine"));
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
        PlayerPrefs.SetInt("Gold", goldAmount);
        PlayerPrefs.SetInt("Silver", silverAmount);
        PlayerPrefs.SetInt("Stone", stoneAmount);
        PlayerPrefs.SetInt("Citizens", citizensAmount);

        PlayerPrefs.SetInt("Warehouse", warehouse_GO.GetComponent<BuildingController>().GetLevel());
        PlayerPrefs.SetInt("Sheriff", sheriff_GO.GetComponent<BuildingController>().GetLevel());
        PlayerPrefs.SetInt("Saloon", saloon_GO.GetComponent<BuildingController>().GetLevel());
        PlayerPrefs.SetInt("Stable", stable_GO.GetComponent<BuildingController>().GetLevel());
        PlayerPrefs.SetInt("Home", home_GO.GetComponent<BuildingController>().GetLevel());
        PlayerPrefs.SetInt("Mine", mine_GO.GetComponent<BuildingController>().GetLevel());
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
    public void AddGold(int amount)
    {
        goldAmount += amount;
    }
    public int GetSilverAmount()
    {
        return silverAmount;
    }
    public void AddSilver(int amount)
    {
        silverAmount += amount;
    }
    public int GetStoneAmount()
    {
        return stoneAmount;
    }
    public void AddStone(int amount)
    {
        stoneAmount += amount;
    }
    public int GetCitizensAmount()
    {
        return citizensAmount;
    }
    public void AddCitizens(int amount)
    {
        citizensAmount += amount;
    }
}
