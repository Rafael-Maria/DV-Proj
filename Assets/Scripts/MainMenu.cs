using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loading;
    private Sprite menuLoading;
    private Sprite cityLoading;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject playButtonFake;

    public void NewGame(){
        //reset
        if(PlayerPrefs.HasKey("new")){
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs reseted");
        }
        initiatePlayerPrefs();
        PlayerPrefs.SetInt("new", 0);
        PlayerPrefs.Save();

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

        
        if(PlayerPrefs.HasKey("new")){
            playButton.SetActive(true);
            playButtonFake.SetActive(false);
        }
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

    private void initiatePlayerPrefs(){
        PlayerPrefs.SetInt("GoldAmount", 0);
        PlayerPrefs.SetInt("SilverAmount", 0);
        PlayerPrefs.SetInt("StoneAmount", 0);
        PlayerPrefs.SetInt("CitizensAmount", 10);

        PlayerPrefs.SetInt("WarehouseLvl", 0);
        PlayerPrefs.SetInt("SheriffLvl", 0);
        PlayerPrefs.SetInt("SaloonLvl", 0);
        PlayerPrefs.SetInt("StableLvl", 0);
        PlayerPrefs.SetInt("HomeLvl", 0);
        PlayerPrefs.SetInt("MineLvl", 0);
    }
}
