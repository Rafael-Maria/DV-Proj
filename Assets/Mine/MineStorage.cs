using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStorage : MonoBehaviour
{

    //calc to Send
    //Plan since we don't know the amount of ores of each type
    private float storageSpaceAvailable; //Storage mine itself
    private float storageSpaceOcupy;
    private float amountStone;
    private float amountSilver;
    private float amountGold;
    private float amountStoneSend;
    private float amountSilverSend;
    private float amountGoldSend;
    private float maxAmountToSend;  //Storage transport
    private float actualAmountToSend;

     void Awake(){
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
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addProduct(int codeOre){
        if(storageSpaceOcupy >=storageSpaceAvailable){
            return;
        }
        storageSpaceOcupy++;
        switch (codeOre){
            case 1:
                amountStone++;
                break;
            case 2:
                amountSilver++;
                break;
            case 3:
                amountGold++;
                break;
            default:
                break;
        }
    }

    void Send(){
        if(amountStoneSend == 0 && amountSilverSend == 0 && amountGoldSend == 0){
            return;
        }
        amountGold -= amountGoldSend;
        amountSilver -= amountSilverSend;
        amountStone -= amountStoneSend;
        //Switch Later
        amountGoldSend=0;
        amountSilverSend=0;
        amountStoneSend=0;
    }

    void addStoneToSend(){
        if(amountStoneSend >= amountStone){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        amountStoneSend++;
    }

    void addSilverToSend(){
        if(amountSilverSend >= amountSilver){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        amountSilverSend++;
    }

    void addGoldToSend(){
        if(amountGoldSend >= amountGold){
            return;
        }
        if(actualAmountToSend >= maxAmountToSend){
            return;
        }
        amountGoldSend++;
    }

}
