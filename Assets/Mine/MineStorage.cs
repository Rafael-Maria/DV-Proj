using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineStorage : MonoBehaviour
{

    //calc to Send
    //Plan since we don't know the amount of ores of each type
    private float storageSpaceAvailable;
    private float storageSpaceOcupy;
    private float amountStone;
    private float amountSilver;
    private float amountGold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addProduct(int codeOre){
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

}
