using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinaController : MonoBehaviour
{
    [SerializeField] private CapacityValues capacityValues;

    [SerializeField] private int mineLevel;
    [SerializeField] private Text mineLevelText;
    [SerializeField] private Text capacityText;
    [SerializeField] private Text amountAddedText;

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip gold;
    [SerializeField] private AudioClip silver;
    [SerializeField] private AudioClip stone;

    private GameController gameController;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>();

        mineLevel = PlayerPrefs.GetInt("Mina");
        mineLevelText.text = "Mine Level: " + (mineLevel+1).ToString(); //Porque começa no 0, visualmente seria level 1
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log((string.Format("Stone: {0} - Silver: {1} - Gold: {2}", PlayerPrefs.GetInt("Stone"), PlayerPrefs.GetInt("Silver"), PlayerPrefs.GetInt("Gold"))));
        }
    }

    public void Mine()
    {
        capacityText.gameObject.SetActive(false);
        int randomPickaxeNum = Random.Range(0, 3);
        string pickaxeSound = "Mina/pickaxe" + randomPickaxeNum;
        sfxAudioSource.PlayOneShot((AudioClip)Resources.Load(pickaxeSound));
        int currentStoneAmount = PlayerPrefs.GetInt("StoneAmount");
        int currentSilverAmount = PlayerPrefs.GetInt("SilverAmount");
        int currentGoldAmount = PlayerPrefs.GetInt("GoldAmount");

        int luckyNumber = Random.Range(4, 10);
        if (luckyNumber > 5) { 
            if (mineLevel == 0) //Stone
            {
                if(currentStoneAmount < capacityValues.GetStoneMaxCapacity(gameController.getWarehouseLevel())) {
                    amountAddedText.gameObject.SetActive(true);
                    currentStoneAmount += 5;
                    amountAddedText.text = "+5 Stone";
                    PlayerPrefs.SetInt("StoneAmount", currentStoneAmount);
                    StartCoroutine(hideAddedAmount(1.2f));
                } else
                { 
                    capacityText.text = "Maximum Stone Capacity Reached!";
                    capacityText.gameObject.SetActive(true);
                }
            }
            else if (mineLevel == 1)//Stone and Silver
            {
                int opt = Random.Range(0, 10);
                if(opt >= 5) //Stone
                {
                    if (currentStoneAmount < capacityValues.GetStoneMaxCapacity(gameController.getWarehouseLevel()))
                    {
                        amountAddedText.gameObject.SetActive(true);
                        currentStoneAmount += 15;
                        amountAddedText.text = "+15 Stone";
                        PlayerPrefs.SetInt("StoneAmount", currentStoneAmount);
                        StartCoroutine(hideAddedAmount(1.2f));
                    } else
                    {
                        capacityText.text = "Maximum Stone Capacity Reached!";
                        capacityText.gameObject.SetActive(true);
                    }
                } else //Silver
                {
                    if(currentSilverAmount < capacityValues.GetSilverMaxCapacity(gameController.getWarehouseLevel())) {
                        amountAddedText.gameObject.SetActive(true);
                        currentSilverAmount += 15;
                        amountAddedText.text = "+15 Silver";
                        PlayerPrefs.SetInt("SilverAmount", currentSilverAmount);
                        StartCoroutine(hideAddedAmount(1.2f));
                    } else
                    {
                        capacityText.text = "Maximum Silver Capacity Reached!";
                        capacityText.gameObject.SetActive(true);
                    }
                }
            }
            else if (mineLevel == 2) //Stone, Silver and Gold
            {
                int opt = Random.Range(0, 25);
                if (opt >= 5) //Stone
                {
                    if(currentStoneAmount < capacityValues.GetStoneMaxCapacity(gameController.getWarehouseLevel())) {
                        amountAddedText.gameObject.SetActive(true);
                        currentStoneAmount += 30;
                        amountAddedText.text = "+30 Stone";
                        PlayerPrefs.SetInt("StoneAmount", currentStoneAmount);
                        StartCoroutine(hideAddedAmount(1.2f));
                    } else
                    {
                        capacityText.text = "Maximum Stone Capacity Reached!";
                        capacityText.gameObject.SetActive(true);
                    }
                }
                else if(opt >= 10) //Silver
                {
                    if(currentSilverAmount < capacityValues.GetSilverMaxCapacity(gameController.getWarehouseLevel())) {
                        amountAddedText.gameObject.SetActive(true);
                        currentSilverAmount += 25;
                        amountAddedText.text = "+25 Silver";
                        PlayerPrefs.SetInt("SilverAmount", currentSilverAmount);
                        StartCoroutine(hideAddedAmount(1.2f));
                    } else
                    {
                        capacityText.text = "Maximum Silver Capacity Reached!";
                        capacityText.gameObject.SetActive(true);
                    }
                } else //Gold
                {
                    if(currentGoldAmount < capacityValues.GetGoldMaxCapacity(gameController.getWarehouseLevel())) {
                        amountAddedText.gameObject.SetActive(true);
                        currentGoldAmount += 20;
                        amountAddedText.text = "+20 Gold";
                        PlayerPrefs.SetInt("GoldAmount", currentGoldAmount);
                        StartCoroutine(hideAddedAmount(1.2f));
                    } else
                    {
                        capacityText.text = "Maximum Gold Capacity Reached!";
                        capacityText.gameObject.SetActive(true);
                    }
                }
            }
        }

        //Debug.Log((string.Format(">>>> Final >>>> Stone: {0} - Silver: {1} - Gold: {2}", currentStoneAmount, currentSilverAmount, currentGoldAmount)));
    }
    IEnumerator hideAddedAmount(float secs)
    {
        yield return new WaitForSeconds(secs);
        amountAddedText.gameObject.SetActive(false);
    }

}
