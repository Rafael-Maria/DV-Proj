using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class City_Menus: MonoBehaviour {

    private GameController gameController;

    /*[Header("Resources")]
    private int stone;
    [SerializeField] private GameObject stoneCounter;
    private int silver;
    [SerializeField] private GameObject silverCounter;
    private int gold;
    [SerializeField] private GameObject goldCounter;*/

    [Header("Boards")]
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject navigation;
    [SerializeField] private GameObject navigation_optional;
    [SerializeField] private GameObject upgradeMenu;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 startPos2;
    private Vector3 endPos2;
    private float distance;
    private float duration;
    private float currentTime;
    private bool isPressed;
    private bool goUp;
    private bool validation;

    void Start(){
        //Resources
        /*stone = 80;
        gold = 40;
        silver = 25;*/
        gameController = FindObjectOfType<GameController>();

        // Menus ---------------------------------------------------
        validation = true;
        goUp = false;
        isPressed = false;
        currentTime = 0;
        duration= 1.1f;
        distance= 470f;
        startPos = board.transform.position;
        endPos = board.transform.position + Vector3.down * distance;
        startPos2 = endPos;
        endPos2 = startPos;
    }

    public void pressed(){
        if(validation == true){
            currentTime = 0;
            validation = false;
            isPressed = true;
            //navigation.SetActive(false);
            //navigation_optional.SetActive(true);
        }
    }

    public void close(){
        if(validation == true && goUp == false){
            currentTime=0;
            validation = false;
            isPressed = true;
            goUp = true;
            //navigation.SetActive(false);
            //navigation_optional.SetActive(true);
        }
    }

    void Update(){
        //Resources
        GameObject.FindWithTag("stone_counter").GetComponent<UnityEngine.UI.Text>().text = gameController.GetStoneAmount().ToString();
        GameObject.FindWithTag("gold_counter").GetComponent<UnityEngine.UI.Text>().text = gameController.GetGoldAmount().ToString(); 
        GameObject.FindWithTag("silver_counter").GetComponent<UnityEngine.UI.Text>().text = gameController.GetSilverAmount().ToString();
        GameObject.FindWithTag("citizens_counter").GetComponent<UnityEngine.UI.Text>().text = gameController.GetCitizensAmount().ToString();

        // Menus ------------------------------------------------------
        if(isPressed == true && goUp == false && validation == false) {
            currentTime += Time.deltaTime;
            if(currentTime >= duration){
                isPressed = false;
                goUp = true;
                validation = true;
                //navigation.SetActive(true);
                //navigation_optional.SetActive(false);
            }

            float Perc = currentTime/duration;
            board.transform.position = Vector3.Lerp(startPos,endPos,Perc);
        }
        if(isPressed == true && goUp == true && validation == false) {
            currentTime += Time.deltaTime;
            if(currentTime >= duration){
                Debug.Log("aqui");
                isPressed = false;
                goUp = false;
                validation = true;
                //navigation.SetActive(true);
                //navigation_optional.SetActive(false);
                //boardUpgrade.SetActive(false);
                startPos = board.transform.position;
                endPos = board.transform.position + Vector3.down * distance;
                startPos2 = endPos;
                endPos2 = startPos;
            }


            float Perc = currentTime/duration;
            board.transform.position = Vector3.Lerp(startPos2,endPos2,Perc);

            Debug.Log(name);
            if(name.Contains("_")){
                upgradeMenu.SetActive(false);
            }
        }
    }
}