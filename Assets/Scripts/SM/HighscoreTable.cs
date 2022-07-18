using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class HighscoreTable : MonoBehaviour {

    [SerializeField] private GameObject entryContainer;
    [SerializeField] private GameObject entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private bool first = true;

    private void Awake() {
        if(first){
            sort();
        }
    }

    public void sort(){
        //entryContainer = transform.Find("highscoreEntryContainer");
        //entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        //entryTemplate.gameObject.SetActive(false);
        //entryTemplate.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = new Highscores() {
            highscoreEntryList = new List<HighscoreEntry>()
        };
        highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        foreach (Transform child in entryContainer.transform)
        {
            if(child.CompareTag("clone")) { 
                Destroy(child.gameObject);
            }
        }


        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer.transform, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        if(transformList.Count < 12){
            float templateHeight = 82f;
            Transform entryTransform = Instantiate(entryTemplate.transform, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

        
            int rank = transformList.Count + 1;
            string rankString;
            switch (rank) {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("posText").GetComponent<Text>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("nameText").GetComponent<Text>().text = name;

            // Set background visible odds and evens, easier to read
            entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        
            // Highlight First
            if (rank == 1) {
                entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
            }

            // Set tropy
            switch (rank) {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().color = getColorFromString("FFD200");
                break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().color = getColorFromString("C6C6C6");
                break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().color = getColorFromString("B76F56");
                break;

            }

            entryTransform.tag = "clone";
            transformList.Add(entryTransform);
        }
    }

    public void AddHighscoreEntry(int score, string name) {
        if(score != 0 || first == true){
            // Load saved Highscores
            string jsonString = PlayerPrefs.GetString("highscoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            // Create HighscoreEntry
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

            if (highscores == null) {
                // There's no stored table, initialize
                highscores = new Highscores() {
                    highscoreEntryList = new List<HighscoreEntry>()
                };
            }

            // Add new entry to Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);

            // Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
            first = false;
        }
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }

    public static Color getColorFromString(string color) {
		float red = Hex_to_Dec01(color.Substring(0,2));
		float green = Hex_to_Dec01(color.Substring(2,2));
		float blue = Hex_to_Dec01(color.Substring(4,2));
        float alpha = 1f;
        if (color.Length >= 8) {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6,2));
        }
		return new Color(red, green, blue, alpha);
	}

    public static float Hex_to_Dec01(string hex) {
		    return Hex_to_Dec(hex)/255f;
	}

        public static int Hex_to_Dec(string hex) {
		return Convert.ToInt32(hex, 16);
	}
}
