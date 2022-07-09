using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShootingMinigame : MonoBehaviour
{
    [Header("GUI Settings")]
    public Texture2D cursorArrow;

    [Header("Player")]
    public int score;
    public int bullets;
    [SerializeField] private float gameDuration;

    [Header("Game Core Settings")]
    [SerializeField] private Text countdownText;
    [SerializeField] private Text bulletsText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text reloadText;
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
    private int spawnedCactus;
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        InitGame();
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
            } else
            {
                gameDuration -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(gameDuration / 60F);
                int seconds = Mathf.FloorToInt(gameDuration - minutes * 60);
                countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        } else
        {
            countdownText.text = string.Format("{0:0}:{1:00}", 0, 0);
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
    }

    public int GetBullets()
    {
        return bullets;
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
        reloading = false;
        playing = true;
        score = 0;
        bullets = 5;
        spawnedCactus = 0;
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
        bullets = 5;
        reloading = false;
        reloadText.gameObject.SetActive(false);
    }
}
