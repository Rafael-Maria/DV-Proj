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

        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second
        loading.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Play(){
        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second
        loading.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        menuLoading = Resources.Load<Sprite>("Images/Loading_Initial");
        cityLoading = Resources.Load<Sprite>("Images/Loading_city");
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
