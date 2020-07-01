using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public TextMeshProUGUI LoadingText;
    public TextMeshProUGUI NoScoresText;
    public TextMeshProUGUI MenuBestScoreText;
    public Button NewGame;
    public Button MainMenu;
    public Button WorldScores;
    private GameObject HighscoreTable;
    public GameObject HighscoreTableScreen;
    private GameObject WorldHighscoreTable;
    public GameObject BlackAwardPanel;
    public Canvas canvas;
    public GameObject ForTheBestHighscoreWorld;
    private string HighscoreString;
    private bool IsBackPermitted = true;//For exit with devices buttons
    // Start is called before the first frame update
    void Start()
    {
        BlackAwardPanel.SetActive(false);
        NewGame.onClick.AddListener(NewGameClick);
        MainMenu.onClick.AddListener(MainMenuClick);
        WorldScores.onClick.AddListener(WorldScoresClick);
        HighscoreTable = GameObject.FindWithTag("local_score_table");
        WorldHighscoreTable = GameObject.FindWithTag("world_score_table");
        LoadingText.enabled = false;
        //If there is no local scores
        HighscoreString = PlayerPrefs.GetString("highscoreTable");
        if (HighscoreString == "")
        {

            NoScoresText.text = "There no scores";
        }
        else
        {
            NoScoresText.text = "";
        }
    }

    IEnumerator DestroyAward(GameObject Award)
    {

        yield return new WaitForSeconds(5);
        IsBackPermitted = true;
        BlackAwardPanel.SetActive(false);
        Destroy(Award);

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("IsAnyGamePlayed") == 1)
        {
            if (PlayerPrefs.GetInt("IsWorldAwardReceived") != 1 && PlayerPrefs.GetInt("BestWorldHighscore") == PlayerPrefs.GetInt("score") && PlayerPrefs.GetInt("BestWorldHighscore") > 0 && PlayerPrefs.GetInt("score") > 0) // We have already kept the highest world record in the database. If the just scored points are equal to the best world record, then the player set the highest world record and received award If the award has not yet been received. Also, the score should not be zero 
            {
                IsBackPermitted = false;//  We block the actions of the device button while the award image is active
                var Award = Instantiate(ForTheBestHighscoreWorld, new Vector3(0, 20, 0), Quaternion.identity) as GameObject;
                Award.transform.SetParent(canvas.transform, false);
                BlackAwardPanel.SetActive(true);
                PlayerPrefs.SetInt("IsWorldAwardReceived", 1);// We establish that the award is received(to receive the award only for the first time)
                StartCoroutine(DestroyAward(Award));
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HighscoreTableScreen.active)
            {
                if (IsBackPermitted)// if back device button can be used now
                {
                    HighscoreTableScreen.SetActive(false);
                    //Refreshing Best score information
                    if (PlayerPrefs.GetInt("BestScore") > 0)
                    {
                        MenuBestScoreText.text = "Highscore: " + PlayerPrefs.GetInt("BestScore").ToString();
                    }
                }
            }
        }

    }


    void NewGameClick()
    {
        // If the tutorial is completed, the game starts.If not, the tutorial starts.
        int IsTutorialCompleted = PlayerPrefs.GetInt("IsCompleted");
        if (IsTutorialCompleted == 0)
        {
            SceneManager.LoadScene("TutorialLevelScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void MainMenuClick()
    {

        HighscoreTableScreen.SetActive(false);
        //Refreshing Best score information
        if (PlayerPrefs.GetInt("BestScore") > 0)
        {
            MenuBestScoreText.text = "Highscore: " + PlayerPrefs.GetInt("BestScore").ToString();
        }

    }
    void WorldScoresClick()//Changing the world records and local records screens by the button
    {
        if (WorldScores.GetComponentInChildren<Text>().text == "WORLD SCORES")
        {
            HighscoreTable.SetActive(false);
            WorldHighscoreTable.SetActive(true);
            WorldScores.GetComponentInChildren<Text>().text = "LOCAL SCORES";
            NoScoresText.enabled = false;
            LoadingText.enabled = true;

        }
        else if (WorldScores.GetComponentInChildren<Text>().text == "LOCAL SCORES")
        {
            WorldHighscoreTable.SetActive(false);
            HighscoreTable.SetActive(true);
            WorldScores.GetComponentInChildren<Text>().text = "WORLD SCORES";
            LoadingText.enabled = false;
            NoScoresText.enabled = true;
        }

    }
}
