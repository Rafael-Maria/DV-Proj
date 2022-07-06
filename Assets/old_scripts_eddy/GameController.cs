using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    SaveLoadSystem sls;
    void Start()
    {
        sls = FindObjectOfType<SaveLoadSystem>();
        sls.Load();
    }

    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        sls.Save();
    }
}
