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
        setLuck(PlayerPrefs.GetInt("MineLevel"));
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space)){
            mine();
         }
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
        PlayerPrefs.SetInt("MineLevel",mineLevel);
        mineStorage.GetComponent<MineStorage>().upgradeMine(mineLevel);
    }

    /*private bool validateUpgrade(){
        switch (GetLevel()){
            case 0:
                //Debug.Log("stone: " + gameController.GetStoneAmount());
                if(gameController.GetStoneAmount() >= 90 && gameController.GetSilverAmount() == 0 && gameController.GetGoldAmount() == 0){
                    //Debug.Log("NICEEE");
                    gameController.RemoveStone(90);
                    return true;
                }
                break;
            case 1:
                if(gameController.GetStoneAmount() >= 550 && gameController.GetSilverAmount() >= 300 && gameController.GetGoldAmount() == 0){
                    gameController.RemoveStone(550);
                    gameController.RemoveSilver(300);
                    return true;
                }
                break;
            case 2:
                if(gameController.GetStoneAmount() >= 1500 && gameController.GetSilverAmount() >= 800 && gameController.GetGoldAmount() == 0){
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
    }*/

    void setLuck(int mineLevel){
        this.mineLevel = mineLevel;
        switch(mineLevel){
            case 0:
                StoneLuck = 120;
                SilverLuck = 200;
                break;
            case 1:
                StoneLuck = 80;
                SilverLuck = 140;
                break;
            case 2:
                StoneLuck = 60;
                SilverLuck = 120;
                break;
            case 3:
                StoneLuck = 50;
                SilverLuck = 80;
                break;
            case 4:
                StoneLuck = 33;
                SilverLuck = 66;
                break;
        }
        mineStorage.GetComponent<MineStorage>().setMine(mineLevel);
    }
}
