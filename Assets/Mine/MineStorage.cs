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

    bool leavebool;

     void Awake(){
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
        //max amount to send and storageSpaceAvailable
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
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
        if(PlayerPrefs.GetInt("TimeMine") > 0 && leavebool){
            timeTakes = PlayerPrefs.GetInt("TimeMine");
            leavebool = true;
            StartCoroutine(Send());
        }
        sending = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("TimeMine") > 0){
            timeTakes = PlayerPrefs.GetInt("TimeMine");
            PlayerPrefs.SetInt("TimeMine",0);
            leavebool = false;
            StartCoroutine(Send());
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
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
            shiftPress = true;
        }else{
             shiftPress = false;
        }
    }

    public void addProduct(int codeOre){
        if(storageSpaceOcupy >=storageSpaceAvailable){
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
            yield return new WaitForSeconds(timeTakes);
            storageSpaceOcupy = storageSpaceOcupy - amountGoldSend - amountSilverSend - amountStoneSend;
            
            GameController main = controller.GetComponent<GameController>();
        //Gold
            int goldExc = main.AddGold(amountGoldSend);
            amountGold -= (amountGoldSend + goldExc);
            amountGoldSend = goldExc;
            PlayerPrefs.SetInt("GoldSend",amountGoldSend);
            GoldSend.text = amountGoldSend.ToString();
            PlayerPrefs.SetInt("GoldMine", amountGold);
            GoldMine.text = amountGold.ToString();

        //Silver
            int silverExc = main.AddSilver(amountSilverSend);
            amountSilver -= (amountSilverSend + silverExc);
            amountSilverSend = silverExc;
            PlayerPrefs.SetInt("SilverSend",amountSilverSend);
            SilverSend.text = amountSilverSend.ToString();
            PlayerPrefs.SetInt("SilverMine", amountSilver);
            SilverMine.text = amountSilver.ToString();

        //Stone
            int stoneExc = main.AddStone(amountStoneSend);
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
        if(amountStoneSend >= amountStone){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        if(shiftPress){
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
        if(shiftPress){
            timeTakes -= (5 * amountStoneSend);
            actualAmountToSend-=amountStoneSend;
            amountStoneSend=0;
        }else{
            amountStoneSend--;
            timeTakes -= 5;
            actualAmountToSend--;
        }
        StoneSend.text = amountStoneSend.ToString();
        PlayerPrefs.SetInt("StoneSend",amountStoneSend);
    }

    public void addSilverToSend(){
        if(sending){
            return;
        }
        if(amountSilverSend >= amountSilver){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        if(shiftPress){
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
        if(shiftPress){
            timeTakes -= (10 * amountSilverSend);
            actualAmountToSend-=amountSilverSend;
            amountSilverSend=0;
        }else{
            amountSilverSend--;
            actualAmountToSend--;
            timeTakes -= 10;
        }
        SilverSend.text = amountSilverSend.ToString();
        PlayerPrefs.SetInt("SilverSend",amountSilverSend);
    }

    public void addGoldToSend(){
        if(sending){
            return;
        }
        if(amountGoldSend >= amountGold){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        if(shiftPress){
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
        if(shiftPress){
            timeTakes -= (15 * amountGoldSend);
            actualAmountToSend-=amountGoldSend;
            amountGoldSend=0;
        }else{
            amountGoldSend--;
            actualAmountToSend--;
            timeTakes -= 15;
        }
        GoldSend.text = amountGoldSend.ToString();
        PlayerPrefs.SetInt("GoldSend",amountGoldSend);
    }

    public void upgrade(){
        switch(transpLevel){
            case 0:
                transpLevel = 1;
                maxAmountToSend = 20;
                break;
            case 1:
                transpLevel = 2;
                maxAmountToSend = 50;
                break;
            case 2:
                transpLevel = 3;
                maxAmountToSend = 80;
                break;
            case 3:
                transpLevel = 4;
                maxAmountToSend = 100;
                break;
        }
        PlayerPrefs.SetInt("TranspLevel",transpLevel);
        MaxTransp.text = "Max Trasnport:" + maxAmountToSend.ToString();
    }

    public void upgradeMine(int mineLevel){
        switch(mineLevel){
            case 0:
                storageSpaceAvailable = 20;
                break;
            case 1:
                storageSpaceAvailable = 80;
                break;
            case 2:
                storageSpaceAvailable = 100;
                break;
            case 3:
                storageSpaceAvailable = 150;
                break;
        }
        gameObject.GetComponent<Text>().text = "Mine Level: "+ mineLevel.ToString();
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
    }

    public void setMine(int mineLevel){
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
        gameObject.GetComponent<Text>().text = "Mine Level: "+ (mineLevel + 1).ToString();
        MaxStorage.text = "Max Capacity: " + storageSpaceAvailable.ToString();
    }

    public void setTrans(int transpLevel){
        switch(transpLevel){
            case 0:
                maxAmountToSend = 10;
                break;
            case 1:
                maxAmountToSend = 20;
                break;
            case 2:
                maxAmountToSend = 50;
                break;
            case 3:
                maxAmountToSend = 80;
                break;
            case 4:
                maxAmountToSend = 100;
                break;
        }
        MaxTransp.text = "Max Trasnport:" + maxAmountToSend.ToString();
    }

    public void leave(){
        PlayerPrefs.SetInt("TimeMine", (int) Mathf.Ceil(timeTakes));
        city.GetComponent<ScenesController>().loadCity();
    }
}
