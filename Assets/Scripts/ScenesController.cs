using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public GameObject loading;
    public Sprite menuLoading;
    public Sprite cityLoading;
    public Sprite gunFightLoading;
    public Sprite mineLoading;    

    // Start is called before the first frame update
    void Start()
    {
        menuLoading = Resources.Load<Sprite>("Images/Loading_Initial");
        cityLoading = Resources.Load<Sprite>("Images/Loading_city");
        gunFightLoading = Resources.Load<Sprite>("Images/Loading_gun_fight");
        mineLoading = Resources.Load<Sprite>("Images/Loading_mine");
    }

    public void loudMenu(){
        //save

        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = menuLoading; 
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second
        loading.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void loudCity(){
        //save

        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second

        loading.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void loudMine(){
        //save

        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = mineLoading;
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second
        loading.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void loadGunFight(){
        //save

        loading.SetActive(true);
        loading.GetComponent<UnityEngine.UI.Image>().sprite = gunFightLoading;
        StartCoroutine(ShowAndHide(loading, 1.0f)); // 1 second.
        loading.SetActive(false);
        SceneManager.LoadScene(3);
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
