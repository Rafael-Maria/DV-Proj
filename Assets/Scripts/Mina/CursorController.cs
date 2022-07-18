using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //private MinaController mc;
    private Vector2 targetPos;
    //private bool mining;

    void Start()
    {
        //mc = FindObjectOfType<MinaController>();
        Cursor.visible = false;
        //transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPos;

        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButton(0)) /*|| Input.GetMouseButton(0))*/
        {
            //if (!mining) { 
                StartCoroutine(AnimateCursor(0.5f));
            //    mc.Mine();
            //}
        }
    }

    IEnumerator AnimateCursor(float secs)
    {
        //mining = true;
        transform.Rotate(0, 0, 45);
        yield return new WaitForSeconds(secs);
        transform.Rotate(0, 0, -45);
        //mining = false;
    }
}
