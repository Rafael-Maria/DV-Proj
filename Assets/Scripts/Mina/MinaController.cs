using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinaController : MonoBehaviour
{
    [SerializeField] private int mineLevel;
    [SerializeField] private Text mineLevelText;

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private List<AudioClip> pickaxeSounds;
    [SerializeField] private AudioClip gold;
    [SerializeField] private AudioClip silver;
    [SerializeField] private AudioClip stone;
    void Start()
    {
        mineLevel = PlayerPrefs.GetInt("Mina");
        mineLevelText.text = "Mine Level: " + (mineLevel+1).ToString();
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
        int randomPickaxeNum = Random.Range(0, 4);
        sfxAudioSource.PlayOneShot(pickaxeSounds[randomPickaxeNum]);
        int currentStoneAmount = PlayerPrefs.GetInt("Stone");
        int currentSilverAmount = PlayerPrefs.GetInt("Silver");
        int currentGoldAmount = PlayerPrefs.GetInt("Gold");

        int luckyNumber = Random.Range(4, 10);
        if (luckyNumber > 5) { 
            if (mineLevel == 0) //Stone
            {
                currentStoneAmount += 5;
                PlayerPrefs.SetInt("Stone", currentStoneAmount);
            }
            else if (mineLevel == 1)//Stone and Silver
            {
                int opt = Random.Range(0, 10);
                if(opt >= 5) //Stone
                {
                    currentStoneAmount += 15;
                    PlayerPrefs.SetInt("Stone", currentStoneAmount);
                } else //Silver
                {
                    currentSilverAmount += 15;
                    PlayerPrefs.SetInt("Silver", currentSilverAmount);
                }
            }
            else if (mineLevel == 2) //Stone, Silver and Gold
            {
                int opt = Random.Range(0, 25);
                if (opt >= 5) //Stone
                {
                    currentStoneAmount += 30;
                    PlayerPrefs.SetInt("Stone", currentStoneAmount);
                }
                else if(opt >= 10) //Silver
                {
                    currentSilverAmount += 25;
                    PlayerPrefs.SetInt("Silver", currentSilverAmount);
                } else //Gold
                {
                    currentGoldAmount += 20;
                    PlayerPrefs.SetInt("Gold", currentGoldAmount);
                }
            }
        }

        //Debug.Log((string.Format(">>>> Final >>>> Stone: {0} - Silver: {1} - Gold: {2}", currentStoneAmount, currentSilverAmount, currentGoldAmount)));
    }
}
