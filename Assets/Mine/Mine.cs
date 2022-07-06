using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Missing change the material of the mine clicker
*/
public class Mine : MonoBehaviour
{
    [SerializeField] MineStorage storage;
    float mineLevel;
    float StoneLuck; 
    float SilverLuck;
    int random;
    int currentOre; // 1-Stone; 2-Silver; 3 - Gold
    [SerializeField] Material stone;
    [SerializeField] Material silver;
    [SerializeField] Material gold;

    void Awake(){
        mineLevel = 0;
        StoneLuck = 100;
        SilverLuck = 200;
        chooseOre();
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

    void chooseOre(){
        random = Random.Range(1, 100);
        if(random <= StoneLuck){
            currentOre=1;
            //put Stone texture
        }else if(random <= SilverLuck){
            currentOre=2;
            //put Silver texture
        }else{
            currentOre=3;
            //put Gold texture
        }
    }

    public void mine(){
        //dependendo do ore;
        //adicionar ao Storage +1, se o Storage ainda estiver espaço;
        //adicionar ao storage o valor do ore, se o Storage ainda estiver espaço;
        //Fazer random para ver o próximo ore (alterar texture)
        storage.addProduct(currentOre);//need to get the  of the ore
        chooseOre();
    }
}
