using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField] private GameObject loading;
    private Sprite menuLoading;
    private Sprite cityLoading;
    private Sprite gunFightLoading;
    private Sprite mineLoading;    

    // Start is called before the first frame update
    void Start()
    {
        menuLoading = Resources.Load<Sprite>("Loading_Initial");
        cityLoading = Resources.Load<Sprite>("Loading_city");
        gunFightLoading = Resources.Load<Sprite>("Loading_gun_fight");
        mineLoading = Resources.Load<Sprite>("Loading_mine");
    }

    public void loadMenu(){
        //save

        loading.GetComponent<UnityEngine.UI.Image>().sprite = menuLoading; 
        StartCoroutine(ShowAndHide(loading, 2.0f, 0)); // 2 second
    }

    public void loadCity(){
        //save

        loading.GetComponent<UnityEngine.UI.Image>().sprite = cityLoading;
        StartCoroutine(ShowAndHide(loading, 2.0f, 1)); // 2 second
    }

    public void loadMine(){
        //save

        loading.GetComponent<UnityEngine.UI.Image>().sprite = mineLoading;
        StartCoroutine(ShowAndHide(loading, 2.0f, 2)); // 2 second
    }

    public void loadGunFight(){
        //save

        loading.GetComponent<UnityEngine.UI.Image>().sprite = gunFightLoading;
        StartCoroutine(ShowAndHide(loading, 2.0f, 3)); // 2 second.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowAndHide(GameObject go, float delay, int index){
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
        SceneManager.LoadScene(index);
    }
}
