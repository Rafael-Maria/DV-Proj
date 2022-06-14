using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    MineStorage storage;
    float mineLevel;
    float StoneLuck; 
    float SilverLuck;
    int random;
    void Awake(){

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void mine(){
        //dependendo do ore;
        //adicionar ao Storage +1, se o Storage ainda estiver espaço;
        //adicionar ao storage o valor do ore, se o Storage ainda estiver espaço;
        //Fazer random para ver o próximo ore (alterar texture)
        storage.addProduct();//need to get the  of the ore
        random = Random.Range(1, 100);
        if(random <= StoneLuck){
            //put Stone;
        }else if(random <= SilverLuck){
            //put Silver
        }else{
            //put Gold
        }
    }
}
