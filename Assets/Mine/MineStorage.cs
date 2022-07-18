using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineStorage : MonoBehaviour
{
    //In add product Add time it takes to send - more material -> more Time
    //calc to Send
    //Plan since we don't know the amount of ores of each type
    private int transpLevel;
    private int storageSpaceAvailable; //Storage mine itself
    private int storageSpaceOcupy;
    private int amountStone;
    private int amountSilver;
    private int amountGold;
    private int amountStoneSend;
    private int amountSilverSend;
    private int amountGoldSend;
    private int maxAmountToSend;  //Storage transport
    private int actualAmountToSend;
    //Time to send
    private float timeTakes; //in seconds
    private bool sending;
     private bool shiftPress;

    //Text fields
    [SerializeField] private Text StoneMine;
    [SerializeField] private Text GoldMine;
    [SerializeField] private Text SilverMine;
    [SerializeField] private Text StoneSend;
    [SerializeField] private Text GoldSend;
    [SerializeField] private Text SilverSend;
    [SerializeField] private Text MaxStorage;
    [SerializeField] private Text MaxTransp;

    //buttons
    [SerializeField] private Button SendResources;
    [SerializeField] private Button SendGold;
    [SerializeField] private Button SendSilver;
    [SerializeField] private Button SendStone;
    [SerializeField] private Button removeGold;
    [SerializeField] private Button removeSilver;
    [SerializeField] private Button removeStone;
    [SerializeField] private Button upgradingButton;
    [SerializeField] private Text timeLeft;

    [SerializeField] GameObject controller;
    [SerializeField] GameObject city;
    [SerializeField] GameObject robbedMsg;
    [SerializeField] private GameObject textForNoCapacity;

    bool leavebool;

     void Awake(){
        //PlayerPrefs.SetInt("TimeMine",0);
        transpLevel=0;
        storageSpaceAvailable = 10;
        storageSpaceOcupy = 0;
        amountStone = 0;
        amountSilver = 0;
        amountGold = 0;
        amountStoneSend = 0;
        amountSilverSend = 0;
        amountGoldSend = 0;
        maxAmountToSend = 10;
        actualAmountToSend = 0;
        timeTakes = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Mine level: " + PlayerPrefs.GetInt("MineLvl"));
        setMine(PlayerPrefs.GetInt("MineLvl"));
        //max amount to send and storageSpaceAvailable
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
        Debug.Log("Max Capacity: " + storageSpaceAvailable);
        setTrans(PlayerPrefs.GetInt("TranspLevel"));
        amountStone = PlayerPrefs.GetInt("StoneMine");
        amountSilver = PlayerPrefs.GetInt("SilverMine");
        amountGold = PlayerPrefs.GetInt("GoldMine");
        amountStoneSend = PlayerPrefs.GetInt("StoneSend");
        amountSilverSend = PlayerPrefs.GetInt("SilverSend");
        amountGoldSend = PlayerPrefs.GetInt("GoldSend");
        storageSpaceOcupy = (amountStone + amountSilver + amountGold);
        actualAmountToSend = (amountStoneSend + amountSilverSend + amountGoldSend);
        timeTakes = ((5 * amountStoneSend) + (10 * amountSilverSend) + (15 * amountGoldSend));
        leavebool = true;
        int temp = PlayerPrefs.GetInt("TimeMine");
        Debug.Log("temp" + temp);
        sending = false;
        if(temp > 0 && leavebool){
            timeTakes = PlayerPrefs.GetInt("TimeMine");
            Debug.Log("Enter if");
            leavebool = true;
            sending = true;
            SendButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Z)){
            shiftPress = true;
        }else{
             shiftPress = false;
        }

        if(sending && timeTakes > 0){
            timeTakes -= Time.deltaTime;
            if(timeTakes > 60){
            timeLeft.text = Mathf.Floor((timeTakes / 60)).ToString() + ":" + (timeTakes - ( Mathf.Floor((timeTakes / 60)) * 60) );
            }else{
                timeLeft.text = timeTakes.ToString("F0");
            }
            //update button telling time
        }
    }

    public void addProduct(int codeOre){
        if(storageSpaceOcupy >= storageSpaceAvailable){
            Debug.Log("ocupy: " + storageSpaceOcupy);
            Debug.Log("available: " + storageSpaceAvailable);
            StartCoroutine(ShowAndHide(textForNoCapacity, 2.0f));
            return;
        }
        storageSpaceOcupy++;
        switch (codeOre){
            case 1:
                amountStone++;
                StoneMine.text = amountStone.ToString();
                PlayerPrefs.SetInt("StoneMine", amountStone);
                break;
            case 2:
                amountSilver++;
                SilverMine.text = amountSilver.ToString();
                PlayerPrefs.SetInt("SilverMine", amountSilver);
                break;
            case 3:
                amountGold++;
                GoldMine.text = amountGold.ToString();
                PlayerPrefs.SetInt("GoldMine", amountGold);
                break;
            default:
                break;
        }
    }

    public void SendButton(){
        Debug.Log("ENTER HERE PLS");
        StartCoroutine(Send());
    }

    IEnumerator Send(){
        if(amountStoneSend == 0 && amountSilverSend == 0 && amountGoldSend == 0){
        }else{

            //block send buttons
            SendResources.interactable = false;
            SendGold.interactable = false;
            SendSilver.interactable = false;
            SendStone.interactable = false;
            upgradingButton.interactable = false;
            removeGold.interactable = false;
            removeSilver.interactable = false;
            removeStone.interactable = false;
            sending = true;
            
            int stableLevel = PlayerPrefs.GetInt("StableLvl");
            float stableUpgrade = 0;
            switch(stableLevel){
                case 0:
                    stableUpgrade = 0.5f;
                    break;
                case 1:
                    stableUpgrade = 0.10f;
                    break;
                case 2:
                    stableUpgrade = 0.20f;
                    break;
                case 3:
                    stableUpgrade = 0.30f;
                    break;
                case 4:
                    stableUpgrade = 0.50f;
                    break;
            }

            timeTakes = timeTakes - (timeTakes * stableUpgrade);

            yield return new WaitForSeconds(timeTakes);
            bool assault = false;
            int randomAssault = Random.Range(0, 25);
            if(PlayerPrefs.GetInt("SheriffLvl") == 2){
                randomAssault = Random.Range(0, 60);
            }
            if(randomAssault == 12){
                assault = true;
            }
            storageSpaceOcupy = storageSpaceOcupy - amountGoldSend - amountSilverSend - amountStoneSend;
            
            GameController main = controller.GetComponent<GameController>();
        //Gold
            int goldExc;
            if(assault && amountGoldSend>1){
                goldExc = main.AddGold(Mathf.FloorToInt(amountGoldSend/2));
            }else{
                goldExc = main.AddGold(amountGoldSend);
            }
            amountGold -= (amountGoldSend + goldExc);
            amountGoldSend = goldExc;
            PlayerPrefs.SetInt("GoldSend",amountGoldSend);
            GoldSend.text = amountGoldSend.ToString();
            PlayerPrefs.SetInt("GoldMine", amountGold);
            GoldMine.text = amountGold.ToString();

        //Silver
        int silverExc;
            if(assault && amountSilverSend>1){
                silverExc = main.AddSilver(Mathf.FloorToInt(amountSilverSend/2));
            }else{
                silverExc = main.AddSilver(amountSilverSend);
            }
            amountSilver -= (amountSilverSend + silverExc);
            amountSilverSend = silverExc;
            PlayerPrefs.SetInt("SilverSend",amountSilverSend);
            SilverSend.text = amountSilverSend.ToString();
            PlayerPrefs.SetInt("SilverMine", amountSilver);
            SilverMine.text = amountSilver.ToString();

        //Stone
            int stoneExc;
            if(assault && amountStoneSend>1){
                stoneExc = main.AddStone(Mathf.FloorToInt(amountStoneSend/2));
            }else{
                stoneExc = main.AddStone(amountStoneSend);
            }
            amountStone -= (amountStoneSend + stoneExc);
            amountStoneSend = stoneExc;
            PlayerPrefs.SetInt("StoneSend",amountStoneSend);
            StoneSend.text = amountStoneSend.ToString();
            PlayerPrefs.SetInt("StoneMine", amountStone);
            StoneMine.text = amountStone.ToString();

        //other parts of storage    
            sending = false;
            PlayerPrefs.SetInt("TimeMine",0);
            actualAmountToSend = (amountStoneSend + amountSilverSend + amountGoldSend);
            timeTakes = ((5 * amountStoneSend) + (10 * amountSilverSend) + (15 * amountGoldSend));
            if(assault){
                //Mesage telling the cargo got robbed
                StartCoroutine(ShowAndHide(robbedMsg, 2.0f));
            }
            assault = false;

            //unlock send buttons
            SendResources.interactable = true;
            SendGold.interactable = true;
            SendSilver.interactable = true;
            SendStone.interactable = true;
            upgradingButton.interactable = true;
            removeGold.interactable = true;
            removeSilver.interactable = true;
            removeStone.interactable = true;
            timeLeft.text = "Send Resources";
        }
    }

    public void addStoneToSend(){
        if(sending){
            return;
        }
        if(amountStoneSend > amountStone){
            shiftPress = false;
            return;
        }
        if(actualAmountToSend > maxAmountToSend){
            shiftPress = false;
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            //Debug.Log("Pressed" + (amountStone - amountStoneSend) + " " + (maxAmountToSend - actualAmountToSend));
            if((amountStone - amountStoneSend) <= (maxAmountToSend - actualAmountToSend)){
                timeTakes += (5 * (amountStone - amountStoneSend));
                actualAmountToSend+=(amountStone - amountStoneSend);
                amountStoneSend = amountStone;
            }else{
                timeTakes += (5 * (maxAmountToSend - actualAmountToSend));
                actualAmountToSend = maxAmountToSend;
                amountStoneSend = (maxAmountToSend - actualAmountToSend);
            }
        }else{
            amountStoneSend++;
            actualAmountToSend++;
            timeTakes += 5;
        }
        shiftPress = false;
            Debug.Log("Pressed" + amountStoneSend);
        StoneSend.text = amountStoneSend.ToString();
        PlayerPrefs.SetInt("StoneSend",amountStoneSend);
    }

    public void removeStoneToSend(){
        if(sending){
            return;
        }
        if(amountStoneSend <= 0){
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            timeTakes = timeTakes - (5 * amountStoneSend);
            actualAmountToSend-=amountStoneSend;
            amountStoneSend=0;
        }else{
            amountStoneSend--;
            timeTakes -= 5;
            actualAmountToSend--;
        }
        shiftPress = false;
        StoneSend.text = amountStoneSend.ToString();
        PlayerPrefs.SetInt("StoneSend",amountStoneSend);
    }

    public void addSilverToSend(){
        if(sending){
            return;
        }
        if(amountSilverSend > amountSilver){
            return;
        }
        if(actualAmountToSend > maxAmountToSend){
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            if((amountSilver - amountSilverSend) <= (maxAmountToSend - actualAmountToSend)){
                timeTakes += (10 * (amountSilver - amountSilverSend));
                actualAmountToSend+=(amountSilver - amountSilverSend);
                amountSilverSend = amountSilver;
            }else{
                timeTakes += (10 * (maxAmountToSend - actualAmountToSend));
                actualAmountToSend = maxAmountToSend;
                amountSilverSend = (maxAmountToSend - actualAmountToSend);
            }
        }else{
            amountSilverSend++;
            actualAmountToSend++;
            timeTakes += 10;
        }
        shiftPress = false;
        SilverSend.text = amountSilverSend.ToString();
        PlayerPrefs.SetInt("SilverSend",amountSilverSend);
    }

    public void removeSilverToSend(){
        if(sending){
            return;
        }
        if(amountSilverSend <= 0){
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            timeTakes -= (10 * amountSilverSend);
            actualAmountToSend-=amountSilverSend;
            amountSilverSend=0;
        }else{
            amountSilverSend--;
            actualAmountToSend--;
            timeTakes -= 10;
        }
        shiftPress = false;
        SilverSend.text = amountSilverSend.ToString();
        PlayerPrefs.SetInt("SilverSend",amountSilverSend);
    }

    public void addGoldToSend(){
        if(sending){
            return;
        }
        if(amountGoldSend > amountGold){
            return;
        }
        if(actualAmountToSend > maxAmountToSend){
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            if((amountGold - amountGoldSend) <= (maxAmountToSend - actualAmountToSend)){
                timeTakes += (15 * (amountGold - amountGoldSend));
                actualAmountToSend+=(amountGold - amountGoldSend);
                amountGoldSend = amountGold;
            }else{
                timeTakes += (15 * (maxAmountToSend - actualAmountToSend));
                actualAmountToSend = maxAmountToSend;
                amountGoldSend = (maxAmountToSend - actualAmountToSend);
            }
        }else{
            amountGoldSend++;
            actualAmountToSend++;
            timeTakes += 15;
        }
        shiftPress = false;
        GoldSend.text = amountGoldSend.ToString();
        PlayerPrefs.SetInt("GoldSend",amountGoldSend);
    }

    public void removeGoldToSend(){
        if(sending){
            return;
        }
        if(amountGoldSend <= 0){
            return;
        }
        if(shiftPress || Input.GetKey(KeyCode.Z)){
            timeTakes -= (15 * amountGoldSend);
            actualAmountToSend-=amountGoldSend;
            amountGoldSend=0;
        }else{
            amountGoldSend--;
            actualAmountToSend--;
            timeTakes -= 15;
        }
        shiftPress = false;
        GoldSend.text = amountGoldSend.ToString();
        PlayerPrefs.SetInt("GoldSend",amountGoldSend);
    }

    public void upgrade(){
        int saloonLevel = PlayerPrefs.GetInt("SaloonLvl");
        int saloonUpgrade = 0;
        switch(saloonLevel){
            case 0:
                break;
            case 1:
                saloonUpgrade = 50;
                break;
            case 2:
                saloonUpgrade = 100;
                break;
        }

        int homeLevel = PlayerPrefs.GetInt("HomeLvl");
        switch(homeLevel){
            case 0:
                transpLevel = 1;
                maxAmountToSend = 25;
                break;
            case 1:
                transpLevel = 2;
                maxAmountToSend = 50;
                break;
            case 2:
                transpLevel = 3;
                maxAmountToSend = 100;
                break;
            case 3:
                transpLevel = 4;
                maxAmountToSend = 200;
                break;
            case 4:
                transpLevel = 4;
                maxAmountToSend = 500;
                break;
        }
        maxAmountToSend = storageSpaceAvailable + saloonUpgrade;
        PlayerPrefs.SetInt("TranspLevel", transpLevel);
        MaxTransp.text = "Max Trasnport:" + maxAmountToSend.ToString();
        setTrans(PlayerPrefs.GetInt("MineLvl"));
    }

    public void upgradeMine(int mineLevel){
        int saloonLevel = PlayerPrefs.GetInt("SaloonLvl");
        int saloonUpgrade = 0;
        switch(saloonLevel){
            case 0:
                break;
            case 1:
                saloonUpgrade = 50;
                break;
            case 2:
                saloonUpgrade = 100;
                break;
        }

        switch(mineLevel){
            case 0:
                storageSpaceAvailable = 10;
                break;
            case 1:
                storageSpaceAvailable = 20;
                break;
            case 2:
                storageSpaceAvailable = 80;
                break;
            case 3:
                storageSpaceAvailable = 100;
                break;
            case 4:
                storageSpaceAvailable = 150;
                break;
        }
        storageSpaceAvailable = storageSpaceAvailable + saloonUpgrade;
        gameObject.GetComponent<Text>().text = "Mine Level: "+ mineLevel.ToString();
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
        setMine(PlayerPrefs.GetInt("MineLvl"));
    }

    public void setMine(int mineLevel){
        int saloonLevel = PlayerPrefs.GetInt("SaloonLvl");
        int saloonUpgrade = 0;
        switch(saloonLevel){
            case 0:
                break;
            case 1:
                saloonUpgrade = 50;
                break;
            case 2:
                saloonUpgrade = 100;
                break;
        }

        switch(mineLevel){
            case 0:
                storageSpaceAvailable = 10;
                break;
            case 1:
                storageSpaceAvailable = 20;
                break;
            case 2:
                storageSpaceAvailable = 80;
                break;
            case 3:
                storageSpaceAvailable = 100;
                break;
            case 4:
                storageSpaceAvailable = 150;
                break;
        }
        storageSpaceAvailable = storageSpaceAvailable + saloonUpgrade;
        gameObject.GetComponent<Text>().text = "Mine Level: "+ (mineLevel + 1).ToString();
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
    }

    public void setTrans(int transpLevel){
        int saloonLevel = PlayerPrefs.GetInt("SaloonLvl");
        int saloonUpgrade = 0;
        switch(saloonLevel){
            case 0:
                break;
            case 1:
                saloonUpgrade = 50;
                break;
            case 2:
                saloonUpgrade = 100;
                break;
        }

        switch(transpLevel){
            case 0:
                maxAmountToSend = 25;
                break;
            case 1:
                maxAmountToSend = 50;
                break;
            case 2:
                maxAmountToSend = 100;
                break;
            case 3:
                maxAmountToSend = 200;
                break;
            case 4:
                maxAmountToSend = 500;
                break;
        }

        maxAmountToSend = storageSpaceAvailable + saloonUpgrade;
        MaxTransp.text = "Max Transport:" + maxAmountToSend.ToString();
    }

    public void leave(){
        Debug.Log("Leave" + (int) Mathf.Ceil(timeTakes));
        int temp = Mathf.CeilToInt(timeTakes);
        PlayerPrefs.SetInt("TimeMine", temp);
        Debug.Log("Player Leave" +PlayerPrefs.GetInt("TimeMine", temp));
        PlayerPrefs.Save();
        city.GetComponent<ScenesController>().loadCity();
    }

    IEnumerator ShowAndHide(GameObject go, float delay){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
