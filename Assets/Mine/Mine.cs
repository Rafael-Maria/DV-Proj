using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*Missing change the material of the mine clicker
*/
public class Mine : MonoBehaviour
{
    int mineLevel;
    float StoneLuck; 
    float SilverLuck;
    int random;
    int currentOre; // 1-Stone; 2-Silver; 3 - Gold
    [SerializeField] Sprite stone;
    [SerializeField] Sprite silver;
    [SerializeField] Sprite gold;
    [SerializeField] GameObject mineStorage;
    [SerializeField] private AudioSource sfxAudioSource;
    void Awake(){
        mineLevel = 0;
        StoneLuck = 120;
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
            //this.image.sprite = stone;
        }else if(random <= SilverLuck){
            currentOre=2;
            //put Silver texture
            //this.image.sprite = silver;
        }else{
            currentOre=3;
            //put Gold texture
            //this.image.sprite = gold;
        }
    }

    public void mine(){
        //dependendo do ore;
        //adicionar ao Storage +1, se o Storage ainda estiver espaço;
        //adicionar ao storage o valor do ore, se o Storage ainda estiver espaço;
        //Fazer random para ver o próximo ore (alterar texture)
        //storage.addProduct(currentOre);//need to get the  of the ore
        mineStorage.GetComponent<MineStorage>().addProduct(currentOre);
        int randomPickaxeNum = Random.Range(0, 3);
        string pickaxeSound = "Mina/pickaxe" + randomPickaxeNum;
        sfxAudioSource.PlayOneShot((AudioClip)Resources.Load(pickaxeSound));
        chooseOre();
    }

    public void upgrade(){
        switch(mineLevel){
            case 0:
                StoneLuck = 80;
                SilverLuck = 140;
                break;
            case 1:
                StoneLuck = 60;
                SilverLuck = 120;
                break;
            case 2:
                StoneLuck = 50;
                SilverLuck = 80;
                break;
            case 3:
                StoneLuck = 33;
                SilverLuck = 66;
                break;
        }
        if(mineLevel < 4){
            mineLevel++;
        }
        mineStorage.GetComponent<MineStorage>().upgradeMine(mineLevel);
    }
}
