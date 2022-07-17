using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMinaValues : MonoBehaviour
{
    [SerializeField] private Text StoneMine;
    [SerializeField] private Text GoldMine;
    [SerializeField] private Text SilverMine;
    [SerializeField] private Text StoneSend;
    [SerializeField] private Text GoldSend;
    [SerializeField] private Text SilverSend;
    // Start is called before the first frame update
    void Start()
    {
        int stoneMineValue = PlayerPrefs.GetInt("StoneMine");
	    int silverMineValue = PlayerPrefs.GetInt("SilverMine");
	    int goldMineValue =PlayerPrefs.GetInt("GoldMine");
        StoneMine.text = stoneMineValue.ToString();
        GoldMine.text = silverMineValue.ToString();
        SilverMine.text = goldMineValue.ToString();
        int stoneSendValue = PlayerPrefs.GetInt("StoneSend");
	    int silverSendValue = PlayerPrefs.GetInt("SilverSend");
	    int goldSendValue =PlayerPrefs.GetInt("GoldSend");
        StoneSend.text = stoneSendValue.ToString();
        GoldSend.text = silverSendValue.ToString();
        SilverSend.text = goldSendValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}