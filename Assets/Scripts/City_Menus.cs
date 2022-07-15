using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class City_Menus: MonoBehaviour {

    private GameController gameController;

    [Header("Resources")]
    private int stone;
    private int gold;
    private int silver;

    [Header("Boards")]
    public GameObject board;
    public GameObject navigation;
    public GameObject navigation_optional;
    public GameObject boardUpgrade;
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
        distance= 256f;
        startPos = board.transform.position;
        endPos = board.transform.position + Vector3.down * distance;
        startPos2 = endPos;
        endPos2 = startPos;
    }

    public void pressed(){
        if(validation == true){
            isPressed = true;
            currentTime = 0;
            validation = false;
            //navigation.SetActive(false);
            //navigation_optional.SetActive(true);
        }
    }

    public void close(){
        if(validation == true){
            validation = false;
            isPressed = true;
            goUp = true;
            currentTime=0;
            startPos2 = board.transform.position;
            endPos2 = board.transform.position + Vector3.up * distance;
            //navigation.SetActive(false);
            //navigation_optional.SetActive(true);
        }
    }

    void Update(){
        //Resources
        GameObject.FindWithTag("stone_counter").GetComponent<Text>().text = gameController.GetStoneAmount().ToString();
        GameObject.FindWithTag("gold_counter").GetComponent<Text>().text = gameController.GetGoldAmount().ToString(); 
        GameObject.FindWithTag("silver_counter").GetComponent<Text>().text = gameController.GetSilverAmount().ToString(); 

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
                isPressed = false;
                goUp = false;
                validation = true;
                //navigation.SetActive(true);
                //navigation_optional.SetActive(false);
                boardUpgrade.SetActive(false);
            }

            float Perc = currentTime/duration;
            board.transform.position = Vector3.Lerp(startPos2,endPos2,Perc);
        }
    }
}