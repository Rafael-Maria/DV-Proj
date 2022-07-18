using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShootingMinigame : MonoBehaviour
{
    [Header("GUI Settings")]
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [Header("Player")]
    public int score;
    public int bullets;
    [SerializeField] private float gameDuration;

    [Header("Game Core Settings")]
    [SerializeField] private Text countdownText;
    [SerializeField] private Text bulletsText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text reloadText;
    [SerializeField] private Text yourScoreText;
    [SerializeField] private AudioClip noAmmo;
    [SerializeField] private AudioClip gunshot;
    [SerializeField] AudioSource sfxAudioSource;

    [Header("Cactus Prefabs")]
    [SerializeField] private GameObject cactusHolder;
    [SerializeField] private GameObject firstCatoPrefab;
    [SerializeField] private GameObject secondCatoPrefab;
    [SerializeField] private GameObject thirdCatoPrefab;
    [SerializeField] private GameObject fourthCatoPrefab;

    [Header("Totem Prefabs")]
    [SerializeField] private GameObject firstTotemPrefab;

    private bool playing;
    private bool reloading;
    //private int spawnedCactus;

    [Header("Menus")]
    [SerializeField] private GameObject inGame;
    [SerializeField] private GameObject InitialMenu;
    [SerializeField] private GameObject endMenu;
    private HighscoreTable highscoreTable;

    [Header("Countdown")]
    [SerializeField] private int countdownTime;
    private int t;
    [SerializeField] private Text countdownDisplay;

    [Header("Countdown")]
    [SerializeField] private GameObject r6;
    [SerializeField] private GameObject r5;
    [SerializeField] private GameObject r4;
    [SerializeField] private GameObject r3;
    [SerializeField] private GameObject r2;
    [SerializeField] private GameObject r1;
    [SerializeField] private GameObject r0;
    [SerializeField] private GameObject red;

    void Start() {
        t = countdownTime;
        highscoreTable = GameObject.FindGameObjectWithTag("highscoreTable").GetComponent<HighscoreTable>();
        endMenu.SetActive(false);
        go();
    }

    void Update()
    {
        if (playing) {
            if (!reloading)
            {
                bulletsText.text = "bullets: " + bullets.ToString();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!reloading && bullets >= 1)
                {
                    FireBullet();
                }
                else
                {
                    reloadText.gameObject.SetActive(true);
                    sfxAudioSource.PlayOneShot(noAmmo);
                }
            }

            scoreText.text = "Score: " + score.ToString();
            
            if (Mathf.Round(gameDuration) <= 0)
            {
                Debug.Log("Time is over!");
                gameDuration = 0;
                playing = false;
                foreach (Transform child in cactusHolder.transform)
                {
                    Destroy(child.gameObject);
                }

                highscoreTable.AddHighscoreEntry(score, "Scr");
                reloadText.gameObject.SetActive(false);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
                inGame.SetActive(false);
                yourScoreText.text = "Score: " + score.ToString();
                highscoreTable.sort();
                endMenu.SetActive(true);
            } else {
                gameDuration -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(gameDuration / 60F);
                int seconds = Mathf.FloorToInt(gameDuration - minutes * 60);
                countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        } else
        {
            countdownText.text = string.Format("{0:0}:{1:00}", 0, 30);
        }

        if (Input.GetKeyDown(KeyCode.R) && bullets == 0)
        {
            reloading = true;
            StartCoroutine(ReloadGun(1));
        }
    }

    public void AddScore(int points)
    {
        score += points;
        //Debug.Log("Current score: " + score);
    }

    public void FireBullet()
    {
        sfxAudioSource.PlayOneShot(gunshot, 0.5F);
        bullets--;
        /*if (bullets == 0)
        {
            reloading = true;
            StartCoroutine(ReloadGun(2));
        }*/
        changeRevolver();
    }

    public int GetBullets()
    {
        return bullets;
    }

    public void changeRevolver(){
        switch(bullets){
            case 6:
                r6.SetActive(true);
                r5.SetActive(true);
                r4.SetActive(true);
                r3.SetActive(true);
                r2.SetActive(true);
                r1.SetActive(true);
                red.SetActive(false);
                break;
            case 5:
                r6.SetActive(false);
                break;
            case 4:
                r5.SetActive(false);
                break;
            case 3:
                r4.SetActive(false);
                break;
            case 2:
                r3.SetActive(false);
                break;
            case 1:
                r2.SetActive(false);
                break;
            case 0:
                r1.SetActive(false);
                red.SetActive(true);
                break;
            default: break;
        }
    }

    IEnumerator wait(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
    }

    public void SpawnEnemy(int type)
    {
        //Position + Offset
        switch(type){
            case 1:
                Instantiate(firstCatoPrefab, new Vector3(Random.Range(-100, -150) + Random.Range(0, -20), Random.Range(-13, -19), -5), Quaternion.identity, cactusHolder.transform);
                break;
            case 2:
                Instantiate(secondCatoPrefab, new Vector3(Random.Range(100, 150) + Random.Range(0, 20), Random.Range(-13, -19), -5), Quaternion.identity, cactusHolder.transform);
                break;
            case 3:
                Instantiate(thirdCatoPrefab, new Vector3(Random.Range(-70, -120) + Random.Range(0, -20), Random.Range(-13, -19), -5), Quaternion.identity, cactusHolder.transform);
                break;
            case 4:
                Instantiate(fourthCatoPrefab, new Vector3(Random.Range(70, 120) + Random.Range(0, 20), Random.Range(-13, -19), -5), Quaternion.identity, cactusHolder.transform);
                break;
            case 5:
                Instantiate(firstTotemPrefab, new Vector3(120 + Random.Range(0, 20), Random.Range(-12, -15), -5), Quaternion.identity, cactusHolder.transform);
                break;
            default: break;
        }
    }

    private void InitGame()
    {
        InitialMenu.SetActive(false);
        reloading = false;
        playing = true;

        score = 0;
        bullets = 6;
        changeRevolver();
        
        //spawnedCactus = 0;
        gameDuration = 30;
        SpawnEnemy(1);
        SpawnEnemy(2);
        SpawnEnemy(3);
        SpawnEnemy(4);
        //SpawnEnemy(5);
    }

    IEnumerator ReloadGun(int secs)
    {
        bulletsText.text = "reloading...";
        yield return new WaitForSeconds(secs);
        bullets = 6;
        reloading = false;
        reloadText.gameObject.SetActive(false);
        changeRevolver();
    }

    public void restartgame(){
        InitialMenu.SetActive(true);
        t = countdownTime;
        go();
    }

    private void go(){
        cursorHotspot = new Vector2(cursorTexture.width / 1.5f, cursorTexture.height / 1.5f);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        StartCoroutine(countdownToStart());
    }

    IEnumerator countdownToStart(){
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        bullets = 6;
        bulletsText.text = "bullets: " + bullets.ToString();
        gameDuration = 30;
        //spawnedCactus = 0;
        changeRevolver();
        inGame.SetActive(true);

        while(t > 0){
            countdownDisplay.text = t.ToString();
            yield return new WaitForSeconds(1f);
            t--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(0.8f);
        InitGame();
    }
}
