using UnityEngine;
using System.Collections;

public class City_Menus: MonoBehaviour {

    public GameObject board;
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

    void Update(){
        if(Input.GetMouseButtonDown(0)) {
            isPressed = true;
        }
        if(isPressed == true) {
            currentTime += Time.deltaTime;
            if(currentTime >= duration){
                isPressed =  false;
            }

            float Perc = currentTime/duration;
            board.transform.position = Vector3.Lerp(startPos,endPos,Perc);
       }
    }
}