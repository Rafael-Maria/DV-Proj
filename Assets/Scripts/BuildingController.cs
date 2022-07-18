using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int resourcesAmount; //Quantos recursos produz

    [SerializeField] private GameObject textForNoResources;
    
    private GameController gameController;
    //private CapacityValues capacityValues;
    private List<Transform> children = new List<Transform>();

    [SerializeField] private GameObject levelTxt;

    [SerializeField] private GameObject menuText;
    [SerializeField] private GameObject upgradeText;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject menu;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();

        if(name != "Game Bar"){
            string fullName = "";
            fullName = (name.ToString() + "Lvl");
            SetLevel(PlayerPrefs.GetInt(fullName));
            foreach (Transform child in transform)
            {
                if(child.CompareTag(name.ToLower())) { 
                    children.Add(child);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        if(name != "Game Bar"){
            //Debug.Log("level: " + level);
            string fullName = "";
            if(level > 0 && name != "Stable"){
                fullName = (name + "_" + this.level);
                foreach(Transform x in children) {
                    if(fullName.Equals(x.name)){
                        x.gameObject.SetActive(false);
                        break;
                    }
                }
            }
            
            this.level = level;
            fullName = (name + "_" + this.level);
            //Debug.Log(gameObject.name);
            //Debug.Log(name);
            //Debug.Log("fullname: " + fullName);
            
            foreach(Transform x in children) {
                //Debug.Log("fullname2: " + fullName);
                //Debug.Log("name: " + x.name);
                if(fullName.Equals(x.name)){
                    //Debug.Log("entrei");
                    x.gameObject.SetActive(true);
                    break;
                }
            }
            changeLevel(GetLevel());
            changeText();
        }
    }

    public int GetResourcesAmount()
    {
        return resourcesAmount;
    }

    public void SetResourcesAmount(int amount)
    {
        this.resourcesAmount = amount;
    }

    public void Reset()
    {
        this.level = 0;
        this.resourcesAmount = 0;
    }

    public void Upgrade(){
        if(validateUpgrade()){
            //Debug.Log("validate level: " + GetLevel());
            SetLevel((GetLevel()+1));
            string fullName = "";
            fullName = (name.ToString() + "Lvl");
            Debug.Log("fullname " + fullName);
            Debug.Log("lvl " + GetLevel());
            gameController.SetLevel(fullName, (GetLevel()+1));
            gameController.saveGame();
        } else {
            Debug.Log("nao entrei");
            StartCoroutine(ShowAndHide(textForNoResources, 2.0f));
            //textForNoResources.SetActive(true);
        }
        //aviso
    }

    public void changeLevel(int lvl){
        string aux = "";
        aux = levelTxt.GetComponent<TextMeshProUGUI>().text;
        //Debug.Log("before: " + aux);
        aux = aux.Substring(0, aux.Length - 1);
        aux = aux + (lvl+1);
        //Debug.Log("after: " + aux);
        levelTxt.GetComponent<TextMeshProUGUI>().text = aux;
    }

    private bool validateUpgrade(){
        //Debug.Log("name: " + name.ToString());
        switch (name.ToString()){
            case "Warehouse":
                //Debug.Log("level: " + GetLevel());
                switch (GetLevel()){
                    case 0:
                        //Debug.Log("stone: " + gameController.GetStoneAmount());
                        if(gameController.GetStoneAmount() >= 90){
                            gameController.RemoveStone(90);
                            return true;
                        }
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 550 && gameController.GetSilverAmount() >= 300){
                            gameController.RemoveStone(550);
                            gameController.RemoveSilver(300);
                            return true;
                        }
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 1500 && gameController.GetSilverAmount() >= 800){
                            gameController.RemoveStone(1500);
                            gameController.RemoveSilver(800);
                            return true;
                        }
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 2900 && gameController.GetSilverAmount() >= 1500 && gameController.GetGoldAmount() >= 800){
                            gameController.RemoveStone(2900);
                            gameController.RemoveSilver(1500);
                            gameController.RemoveGold(800);
                            upgradeButton.SetActive(false);
                            return true;
                        }
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Sheriff":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 2600 && gameController.GetSilverAmount() >= 1150 && gameController.GetGoldAmount() >= 700){
                            gameController.RemoveStone(2600);
                            gameController.RemoveSilver(1150);
                            gameController.RemoveGold(700);
                            upgradeButton.SetActive(false);
                            return true;
                        }
                        break;
                    case 1:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Saloon":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 550 && gameController.GetSilverAmount() >= 300){
                            gameController.RemoveStone(550);
                            gameController.RemoveSilver(300);
                            return true;
                        }
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 9000 && gameController.GetSilverAmount() >= 4100 && gameController.GetGoldAmount() >= 1600){
                            gameController.RemoveStone(9000);
                            gameController.RemoveSilver(4100);
                            gameController.RemoveGold(1600);
                            upgradeButton.SetActive(false);
                            return true;
                        }
                        break;
                    case 2:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Stable":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 400 && gameController.GetSilverAmount() >= 250){
                            gameController.RemoveStone(400);
                            gameController.RemoveSilver(250);
                            return true;
                        }
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 1300 && gameController.GetSilverAmount() >= 750){
                            gameController.RemoveStone(1300);
                            gameController.RemoveSilver(750);
                            return true;
                        }
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 2300 && gameController.GetSilverAmount() >= 1200 && gameController.GetGoldAmount() >= 650){
                            gameController.RemoveStone(2300);
                            gameController.RemoveSilver(1200);
                            gameController.RemoveGold(650);
                            return true;
                        }
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 7000 && gameController.GetSilverAmount() >= 3800 && gameController.GetGoldAmount() >= 1400){
                            gameController.RemoveStone(7000);
                            gameController.RemoveSilver(3800);
                            gameController.RemoveGold(1400);
                            upgradeButton.SetActive(false);
                            return true;
                        }
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Home":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 200 && gameController.GetSilverAmount() >= 90){
                            gameController.RemoveStone(200);
                            gameController.RemoveSilver(90);
                            gameController.AddCitizens(10);
                            return true;
                        }
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 800 && gameController.GetSilverAmount() >= 400){
                            gameController.RemoveStone(800);
                            gameController.RemoveSilver(400);
                            gameController.AddCitizens(20);
                            return true;
                        }
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 1800 && gameController.GetSilverAmount() >= 800 && gameController.GetGoldAmount() >= 400){
                            gameController.RemoveStone(1800);
                            gameController.RemoveSilver(800);
                            gameController.RemoveGold(400);
                            gameController.AddCitizens(40);
                            return true;
                        }
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 5000 && gameController.GetSilverAmount() >= 2800 && gameController.GetGoldAmount() >= 1100){
                            gameController.RemoveStone(5000);
                            gameController.RemoveSilver(2800);
                            gameController.RemoveGold(1100);
                            gameController.AddCitizens(80);
                            upgradeButton.SetActive(false);
                            return true;
                        }
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
        }
        //Debug.Log("NICEEE");
        return false;
    }

    public void changeText(){
        switch (name.ToString()){
            case "Warehouse":
                switch (GetLevel()){
                    case 0:
                        menuText.GetComponent<UnityEngine.UI.Text>().text = "The higher the level of the Warehouse, the more resources you can have.\nStorage: \n100           0           0 ";
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n90             0            0  \nStorage improvement:\nStone: 100 + 500\nSilver: 0 + 350\nGold: 0";
                        break;
                    case 1:
                        menuText.GetComponent<UnityEngine.UI.Text>().text = "The higher the level of the Warehouse, the more resources you can have.\nStorage: \n600       350           0 ";
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n550         300            0    \nStorage improvement:\nStone: 600 + 1000\nSilver: 350 + 450\nGold: 0";
                        break;
                    case 2:
                        menuText.GetComponent<UnityEngine.UI.Text>().text = "The higher the level of the Warehouse, the more resources you can have.\nStorage: \n1600       900           0   ";
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n1500         800            0     \nStorage improvement:\nStone:1600 + 3400\nSilver: 900 + 700\nGold: 0 + 900";
                        break;
                    case 3:
                        menuText.GetComponent<UnityEngine.UI.Text>().text = "The higher the level of the Warehouse, the more resources you can have.\nStorage: \n3000     1600       900   ";
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n2900       1500        800     \nStorage improvement:\nStone: 3000 + 7000\nGold: 50 + 70\nSilver: 30 + 30";
                        break;
                    case 4:
                        menuText.GetComponent<UnityEngine.UI.Text>().text = "The higher the level of the Warehouse, the more resources you can have.\nStorage: \n10000     5000     2000     ";
                        break;
                }
                break;
            case "Sheriff":
                switch (GetLevel()){
                    case 0:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n2600       1150        700     \n\nAn assault chance is reduced by 70%.";
                        break;
                    case 1:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Saloon":
                switch (GetLevel()){
                    case 0:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n550         300             0  \n\nWorker performance is improved by 10%.";
                        break;
                    case 1:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n9000       4100       1600    \n\nWorker performance is improved by 10%.";
                        break;
                    case 2:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Stable":
                switch (GetLevel()){
                    case 0:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n400         250             0  \n\nThe time it takes for resources to reach the city will be reduced by 10%.";
                        break;
                    case 1:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n1300         750             0    \n\nThe time it takes for resources to reach the city will be reduced by 10%.";
                        break;
                    case 2:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n2300       1200         650    \n\nThe time it takes for resources to reach the city will be reduced by 10%.";
                        break;
                    case 3:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n7000       3800       1400    \n\nThe time it takes for resources to reach the city will be reduced by 10%.";
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Home":
                switch (GetLevel()){
                    case 0:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n200           90             0  \nThe city now has twice as many workers.\nCitizens: 10 + 10";
                        break;
                    case 1:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n800         400             0  \nThe city now has twice as many workers.\nCitizens: 20 + 20";
                        break;
                    case 2:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n1800         800         400    \nThe city now has twice as many workers.\nCitizens: 40 + 40";
                        break;
                    case 3:
                        upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Cost:\n5000       2800       1100    \nThe city now has twice as many workers.\nCitizens: 80 + 80";
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
        }
    }

    IEnumerator ShowAndHide(GameObject go, float delay){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
