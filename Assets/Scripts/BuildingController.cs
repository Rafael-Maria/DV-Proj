using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int resourcesAmount; //Quantos recursos produz

    [SerializeField] private GameObject textForNoResources;
    private GameController gameController;
    //private CapacityValues capacityValues;
    private string objectName;
    private ArrayList children = new ArrayList();

    void Start()
    {
        objectName = gameObject.name;
        //Debug.Log(objectName);
        gameController = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        //gameController.AddStone(100);

        if(gameObject.name != "Game Bar"){
            foreach (Transform child in transform)
            {
                if(child.CompareTag(objectName.ToLower())) { 
                    children.Add(child);
                }
            }
        }
        /*foreach(var x in children) {
            Debug.Log(x.ToString());
        }*/
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
        if(gameObject.name != "Game Bar"){
            //Debug.Log(level);
            string fullName = "";
            if(level > 0 && objectName != "Stable"){
                fullName = (objectName + "_" + this.level);
                //Debug.Log(fullName);
                GameObject.Find(fullName).SetActive(false);
            }
            this.level = level;
            fullName = (objectName + "_" + this.level);
            Debug.Log(objectName);
            Debug.Log(fullName);
            GameObject.Find(fullName).SetActive(true);

            //USAR A LISTA PARA DAR SetActive NOS EDIFICIOS CERTOS ----------------------------------------------------------

            //this.level = level;
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
            SetLevel((GetLevel()+1));
            //Debug.Log(GetLevel());
        } else {
            //Debug.Log("nao entrei");
        }
        //aviso
    }

    private bool validateUpgrade(){
        switch (objectName){
            case "Warehouse":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 90 && gameController.GetSilverAmount() == 0 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 550 && gameController.GetSilverAmount() >= 300 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 1500 && gameController.GetSilverAmount() >= 800 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 2900 && gameController.GetSilverAmount() >= 1500 && gameController.GetGoldAmount() >= 800)
                            return true;
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Sheriff":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 2600 && gameController.GetSilverAmount() >= 1150 && gameController.GetGoldAmount() >= 700)
                            return true;
                        break;
                    case 1:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Saloon":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 550 && gameController.GetSilverAmount() >= 300 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 990 && gameController.GetSilverAmount() >= 4100 && gameController.GetGoldAmount() >= 1600)
                            return true;
                        break;
                    case 2:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Stable":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 400 && gameController.GetSilverAmount() >= 250 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 1300 && gameController.GetSilverAmount() >= 7500 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 2300 && gameController.GetSilverAmount() >= 1200 && gameController.GetGoldAmount() >= 650)
                            return true;
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 7000 && gameController.GetSilverAmount() >= 3800 && gameController.GetGoldAmount() >= 1400)
                            return true;
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
            case "Home":
                switch (GetLevel()){
                    case 0:
                        if(gameController.GetStoneAmount() >= 200 && gameController.GetSilverAmount() >= 90 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 1:
                        if(gameController.GetStoneAmount() >= 800 && gameController.GetSilverAmount() >= 400 && gameController.GetGoldAmount() == 0)
                            return true;
                        break;
                    case 2:
                        if(gameController.GetStoneAmount() >= 1800 && gameController.GetSilverAmount() >= 800 && gameController.GetGoldAmount() >= 400)
                            return true;
                        break;
                    case 3:
                        if(gameController.GetStoneAmount() >= 5000 && gameController.GetSilverAmount() >= 2800 && gameController.GetGoldAmount() >= 1100)
                            return true;
                        break;
                    case 4:
                        //ja ta no ultimo nivel (meter aviso)
                        break;
                }
                break;
        }
        return false;
    }
}
