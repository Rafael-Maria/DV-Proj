using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loading;
    public Sprite menuLoading;
    public Sprite cityLoading;

    public void NewGame(){
        //reset

        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 2.0f)); // 2 second
        SceneManager.LoadScene(1);
    }

    public void Play(){
        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 2.0f)); // 2 second
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        menuLoading = Resources.Load<Sprite>("Loading_Initial");
        cityLoading = Resources.Load<Sprite>("Loading_city");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowAndHide(GameObject go, float delay){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
