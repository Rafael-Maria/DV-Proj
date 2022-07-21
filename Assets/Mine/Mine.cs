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
    [SerializeField] private GameObject upgradeText;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject textForNoResources;
    private GameController gameController;

    void Awake(){
        mineLevel = PlayerPrefs.GetInt("MineLvl");
        mineLevel = 0;
        StoneLuck = 120;
        SilverLuck = 200;
        chooseOre();
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();
        mineLevel = PlayerPrefs.GetInt("MineLvl");
        setLuck(mineLevel);
        setTextUpgarde();
        mineStorage.GetComponent<MineStorage>().setMine(PlayerPrefs.GetInt("MineLvl"));
        mineStorage.GetComponent<MineStorage>().setTrans(PlayerPrefs.GetInt("MineLvl"));
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space)){
             //mineStorage.GetComponent<MineStorage>().setMine(PlayerPrefs.GetInt("MineLvl"));
             //mineStorage.GetComponent<MineStorage>().setTrans(PlayerPrefs.GetInt("MineLvl"));
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
        Debug.Log(currentOre);
        mineStorage.GetComponent<MineStorage>().addProduct(currentOre);
        int randomPickaxeNum = Random.Range(0, 3);
        string pickaxeSound = "Mina/pickaxe" + randomPickaxeNum;
        sfxAudioSource.PlayOneShot((AudioClip)Resources.Load(pickaxeSound));
        chooseOre();
    }

    public void upgrade(){
        if(validateUpgrade()){
            gameController.SetLevel("MineLvl", (PlayerPrefs.GetInt("MineLvl") + 1));
            gameController.saveGame();
            setLuck(PlayerPrefs.GetInt("MineLvl"));
            setTextUpgarde();
        } else {
            //Debug.Log("nao entrei");
            StartCoroutine(ShowAndHide(textForNoResources, 2.0f));
            //textForNoResources.SetActive(true);
        }
        mineStorage.GetComponent<MineStorage>().upgradeMine(PlayerPrefs.GetInt("MineLvl"));
        mineStorage.GetComponent<MineStorage>().setMine(PlayerPrefs.GetInt("MineLvl"));
        mineStorage.GetComponent<MineStorage>().setTrans(PlayerPrefs.GetInt("MineLvl"));
        gameController.saveGame();
    }

    private bool validateUpgrade(){
        switch (PlayerPrefs.GetInt("MineLvl")){
            case 0:
                if(gameController.GetStoneAmount() >= 300){
                    gameController.RemoveStone(300);
                    return true;
                }
                break;
            case 1:
                if(gameController.GetStoneAmount() >= 800 && gameController.GetSilverAmount() >= 350){
                    gameController.RemoveStone(800);
                    gameController.RemoveSilver(350);
                    return true;
                }
                break;
            case 2:
                if(gameController.GetStoneAmount() >= 2200 && gameController.GetSilverAmount() >= 1100){
                    gameController.RemoveStone(2200);
                    gameController.RemoveSilver(1100);
                    return true;
                }
                break;
            case 3:
                if(gameController.GetStoneAmount() >= 4000 && gameController.GetSilverAmount() >= 2100 && gameController.GetGoldAmount() >= 750){
                    gameController.RemoveStone(4000);
                    gameController.RemoveSilver(2100);
                    gameController.RemoveGold(750);
                    upgradeButton.SetActive(false);
                    return true;
                }
                break;
            case 4:
                //ja ta no ultimo nivel (meter aviso)
                break;
        }
        return false;
    }

    public void setTextUpgarde(){
        switch (PlayerPrefs.GetInt("MineLvl")){
            case 0:
                upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Probability of mining:\nStone - 100%\nSilver - 0%\nGold - 0%\n\nCost of upgarde:\n300            0         0  ";
                break;
            case 1:
                upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Probability of mining:\nStone - 80%\nSilver - 20%\nGold - 0%\n\nCost of upgarde:\n800        350         0   ";
                break;
            case 2:
                upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Probability of mining:\nStone - 60%\nSilver - 40%\nGold - 0%\n\nCost of upgarde:\n2200     1100         0     ";
                break;
            case 3:
                upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Probability of mining:\nStone - 50%\nSilver - 30%\nGold - 20%\n\nCost of upgarde:\n4000     2100     750    ";
                break;
            case 4:
                upgradeText.GetComponent<UnityEngine.UI.Text>().text = "Probability of mining:\nStone - 33.3%\nSilver - 33.3%\nGold - 33.3%\n\nMax Level";
                break;
        }
    }

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

    IEnumerator ShowAndHide(GameObject go, float delay){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
