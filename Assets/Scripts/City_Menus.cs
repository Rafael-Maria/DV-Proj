using UnityEngine;
using System.Collections;

public class City_Menus: MonoBehaviour {

    public GameObject board;
    public GameObject navigation;
    public GameObject navigation_optional;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance=215f;
    private float duration=1.1f;
    private float currentTime=0;
    private bool isPressed=false;

    void Start(){
        startPos = board.transform.position;
        endPos = board.transform.position + Vector3.down * distance;
    }

    public void pressed(){
        isPressed = true;
        //navigation.SetActive(false);
        //navigation_optional.SetActive(true);
    }

    void Update(){
        if(isPressed == true) {
            currentTime += Time.deltaTime;
            if(currentTime >= duration){
                isPressed = false;
                //navigation.SetActive(true);
                //navigation_optional.SetActive(false);
            }

            float Perc = currentTime/duration;
            board.transform.position = Vector3.Lerp(startPos,endPos,Perc);
       }
    }
}