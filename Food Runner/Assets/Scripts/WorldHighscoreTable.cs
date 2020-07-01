using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class WorldHighscoreTable : MonoBehaviour
{
    //Dreamlo server personal data
    //http://dreamlo.com/lb/DTmewZxvAky-YQMys4zKFwCavEzST1q0auZ8EzjwlbqA
    const string privateCode = "DTmewZxvAky-YQMys4zKFwCavEzST1q0auZ8EzjwlbqA";
    const string publicCode = "5d0102173ebb3b1870d0bb5e";
    const string webURL = "http://dreamlo.com/lb/";

    public LeaderBoard WebScoreBoard;
    public TextMeshProUGUI LoadingText; //  Information about the highscores table(no entries, no connection)
    private Transform entryContainer;
    private Transform entryTemplate;
    private Transform entryScroll;
    private List<Transform> highscoreEntryTransformList;

    private void OnEnable()
    {

        LoadingText.text = "Please, wait...";
        entryContainer = transform.Find("highscoreEntryContainer").Find("scrollEntry").Find("Viewport").Find("Content");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        //  Removing old entries before reloading data
        if (entryContainer.childCount > 1)
        {
            GameObject[] allChildren = new GameObject[entryContainer.childCount];

            for (int i = 1; i < entryContainer.childCount; i++)
            {
                Transform child = entryContainer.GetChild(i);
                allChildren[i] = child.gameObject;

            }

            for (int i = 1; i < allChildren.Length; i++)
            {
                GameObject child = allChildren[i];
                Destroy(child.gameObject);

            }
        }

        DownloadHighscores();

    }

    public static IEnumerator UploadNewHighscore(string username, int score, int levelIndex)//Uploading a new record to the server
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + levelIndex);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            Debug.Log("Upload Successful");
        else
        {
            Debug.LogError("Error uploading: " + www.error);
        }

    }

    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");

    }

    IEnumerator DownloadHighscoresFromDatabase()//Downloading scores from the server
    {

        WWW www = new WWW(webURL + publicCode + "/json/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            string json = RemoveNJsonFields(www.text, 2);
            WebScoreBoard = JsonUtility.FromJson<LeaderBoard>(json);
        }
        else
        {
            LoadingText.text = "Sorry, error occured. Try again.";
        }


        LoadingText.text = " ";
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            LoadingText.text = "Error. Check internet connection!";
        }
        else
        {
            if (WebScoreBoard.entry.Length != 0 && WebScoreBoard != null)
            {//Setting the biggest highscore worldwide
                PlayerPrefs.SetInt("BestWorldHighscore", WebScoreBoard.entry[0].score);
                for (int i = 0; i < WebScoreBoard.entry.Length; i++)//Display records from the server on the screen

                {
                    Entry highscoreEntry = WebScoreBoard.entry[i];
                    CreateEntry(highscoreEntry, entryContainer, i + 1);
                }
            }
            else
            {
                PlayerPrefs.SetInt("BestWorldHighscore", 0);
                LoadingText.text = "Error. Check internet connection!";

            }
        }

    }

    //Could be usefull later
    public List<HighscoreData> RequestHighscoreList(Entry[] entries, int index)
    {
        List<HighscoreData> highscores = new List<HighscoreData>();

        foreach (Entry entry in entries)
        {
            if (entry.seconds.Equals(index))
            {
                HighscoreData hd = new HighscoreData(entry.name, entry.score, entry.seconds);
                highscores.Add(hd);
            }
        }

        return highscores;
    }

    private string RemoveNJsonFields(string source, int n)//Removing unwanted characters from json
    {
        n++;

        int index = source.TakeWhile(c => (n -= (c == '{' ? 1 : 0)) > 0).Count();

        return source.Substring(index, source.Length - (index + 2 - n));
    }

    private void CreateEntry(Entry highscoreEntry, Transform container, int place)//Display records from the server on the screen
    {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * WebScoreBoard.entry.Length);
        entryTransform.gameObject.SetActive(true);

        int rank = place;
        string rankString;
        switch (rank)
        {
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


        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        //highlight first
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

    }
}

//Classes for convenient use
[Serializable]
public class LeaderBoard
{
    public Entry[] entry;
}

[Serializable]
public class Entry
{
    public string name;
    public int score;
    public int seconds;
    public string text;
    public DateTime date;
}

public class HighscoreData
{
    public string username;
    public int score;
    public int levelIndex;
    
    //constructor
    public HighscoreData(string username, int score, int levelIndex)
    {
        this.username = username;
        this.score = score;
        this.levelIndex = levelIndex;
    }
}