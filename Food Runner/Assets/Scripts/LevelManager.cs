using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class LevelManager : MonoBehaviour
{
    public AwardRuntime[] Awards;

    public int score = 0;
    public int UsefulItemsCollected = 0;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    public Button PauseButton;
    public Button ResumeButton;
    public Button MenuButton;
    public Button YesButton;
    public Button NoButton;
    public GameObject PausePanel;
    public GameObject ExitToMenuPanel;
    public Canvas canvas;


    public Slider EffSlider;
    public Slider MusSlider;
    public Slider MasSlider;
    public AudioMixer MasterMixer;
    public GameObject PointsText;
    bool IsBonusTime = true;
    public GameObject[] BonusFood;
    public GameObject PointsLife;
    public GameObject MinusLife;
    public GameObject TutorialEndPanel;
    private LifeController GameLifeController;
    private bool OnPause = false;

    public GameObject PlayerGirl;
    public GameObject PlayerBoy;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("PlayerType") == "Boy")
        {
            PlayerGirl.SetActive(false);
        }
        else
        {
            PlayerBoy.SetActive(false);
        }
        PausePanel.transform.gameObject.SetActive(false);
        ExitToMenuPanel.transform.gameObject.SetActive(false);
        ScoreText.text = "Score: " + score;
        BestScoreText.text = "Highscore: " + PlayerPrefs.GetInt("BestScore").ToString();
        PauseButton.onClick.AddListener(PauseButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
        NoButton.onClick.AddListener(NoButtonClick);
        ResumeButton.onClick.AddListener(ResumeButtonClick);
        MenuButton.onClick.AddListener(MenuButtonClick);
        GameLifeController = FindObjectOfType<LifeController>();



    }
    /*Level manager is used in the tutorial and in a simple game. 
     * In order for the bonuses not to spawn and the tutorial to behave a little differently, 
     * there are checks on the variables assigned by the inspector. 
     * If they are null, then this is for tutorial.
    */
    // Update is called once per frame
    void Update()
    {
        // If bonus food is asigned in inspector, its could be spawn
        if (BonusFood != null && BonusFood.Length != 0)
        {
            if (IsBonusTime)
            {
                StartCoroutine(BonusSpawn());
                IsBonusTime = false;
            }
        }
        // Some different control for back device button for tutorial and simple game
        if (TutorialEndPanel == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ExitToMenuPanel.active)
                {
                    ExitToMenuPanel.SetActive(false);
                }
                else if (PausePanel.active)
                {
                    Time.timeScale = 1f;
                    OnPause = false;
                    PausePanel.SetActive(false);
                }
                else if (!ExitToMenuPanel.active && !PausePanel.active)
                {
                    Time.timeScale = 0f;
                    OnPause = true;

                    //Getting the same volume level that was set previously
                    MusSlider.value = GetLevels("MusicVol");
                    EffSlider.value = GetLevels("EffVol");
                    MasSlider.value = GetLevels("MasterVol");
                    PausePanel.transform.gameObject.SetActive(true);

                }
            }
        }
        else
        {
            if (!TutorialEndPanel.active)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (ExitToMenuPanel.active)
                    {
                        ExitToMenuPanel.SetActive(false);
                    }
                    else if (PausePanel.active)
                    {
                        Time.timeScale = 1f;
                        OnPause = false;
                        PausePanel.SetActive(false);
                    }
                    else if (!ExitToMenuPanel.active && !PausePanel.active)
                    {
                        Time.timeScale = 0f;
                        OnPause = true;
                        MusSlider.value = GetLevels("MusicVol");
                        EffSlider.value = GetLevels("EffVol");
                        MasSlider.value = GetLevels("MasterVol");
                        PausePanel.transform.gameObject.SetActive(true);

                    }
                }
            }
        }
    }

    //Instantiation of animations that appear when collecting healthy and unhealthy food(+10 points, - life)

    //Functions and enumerators are needed both, because the object that the player raises ceases to exist and can no longer perform coroutine, so this is called from the level manager, which exists all the game time.
    public IEnumerator BonusTextCreate(Vector3 Position, int SpawnDalay)
    {
        yield return new WaitForSeconds(0.15f * SpawnDalay);
        var AnimText = Instantiate(PointsText, Position, Quaternion.identity) as GameObject;
        AnimText.transform.SetParent(canvas.transform, false);
    }
    public IEnumerator BonusLifeCreate(Vector3 Position, int SpawnDalay)
    {
        yield return new WaitForSeconds(0.15f * SpawnDalay);
        var AnimText = Instantiate(PointsLife, Position, Quaternion.identity) as GameObject;
        AnimText.transform.SetParent(canvas.transform, false);
    }

    public IEnumerator MinusLifeCreate(Vector3 Position, int SpawnDalay)
    {
        yield return new WaitForSeconds(0.15f * SpawnDalay);
        var AnimText = Instantiate(MinusLife, Position, Quaternion.identity) as GameObject;
        AnimText.transform.SetParent(canvas.transform, false);
    }
    public void BonusTextCreateFunction(int SpawnDalay, Transform StartTransform)
    {
        Vector3 Position = new Vector3(StartTransform.position.x, StartTransform.position.y, StartTransform.position.z);
        StartCoroutine(BonusTextCreate(Position, SpawnDalay));
    }
    public void BonusLifeCreateFunction(int SpawnDalay)
    {
        Vector3 Position = new Vector3(-250, -50, 0);
        StartCoroutine(BonusLifeCreate(Position, SpawnDalay));
    }
    public void MinusLifeCreateFunction(int SpawnDalay, Transform StartTransform)
    {
        Vector3 Position = new Vector3(StartTransform.position.x + 40, StartTransform.position.y, StartTransform.position.z);
        StartCoroutine(MinusLifeCreate(Position, SpawnDalay));
    }
    IEnumerator BonusSpawn()
    {
        //spawn bonus after each minute of game
        yield return new WaitForSeconds(60);
        if (!GameLifeController.IsSetted) // If the game is not over
        {
            int rand = Random.Range(0, BonusFood.Length);
            var BonusItem = Instantiate(BonusFood[rand], new Vector3(23, 0, 0), Quaternion.identity);
            //make it bigger
            BonusItem.transform.localScale = new Vector3(BonusItem.transform.localScale.x * 1.5f, BonusItem.transform.localScale.y * 1.5f, BonusItem.transform.localScale.z * 1.5f);
            BonusItem.tag = "bonus";
            IsBonusTime = true;
        }

    }
    public void ChangeScore(int NumberOfPoints)

    {
        //change score
        score += NumberOfPoints;
        if (score < 0)
        {
            score = 0;
        }
        ScoreText.text = "Score: " + score;

        //Display rewards on screen during the game

        foreach (AwardRuntime Sample in Awards)
        {
            if (score >= Sample.Points && PlayerPrefs.GetInt(Sample.PlayerPrefsVariable) != 1)
            {
                PlayerPrefs.SetInt(Sample.PlayerPrefsVariable, 1);
                var Award = Instantiate(Sample.AwardObject, new Vector3(-300, 100, 0), Quaternion.identity) as GameObject;
                Award.transform.SetParent(canvas.transform, false);
                StartCoroutine(DestroyAward(Award));
            }
        }
       
    }
    IEnumerator DestroyAward(GameObject Award) //destroy award after showing
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(Award);
    }



    public void PauseButtonClick()
    {
        //setting pause
        Time.timeScale = 0f;
        OnPause = true;
        //Getting the same volume level that was set previously
        MusSlider.value = GetLevels("MusicVol");

        EffSlider.value = GetLevels("EffVol");

        MasSlider.value = GetLevels("MasterVol");


        PausePanel.transform.gameObject.SetActive(true);

    }

    float GetLevels(string name)
    {
        float value;
        bool result = MasterMixer.GetFloat(name, out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }


    public void ResumeButtonClick()
    {
        Time.timeScale = 1f;
        OnPause = false;
        PausePanel.transform.gameObject.SetActive(false);
    }

    public void MenuButtonClick()
    {
        ExitToMenuPanel.SetActive(true);
    }

    //If a player wants to leave the game and go to the menu
    public void YesButtonClick()
    {
        Time.timeScale = 1f;
        OnPause = false;
        SceneManager.LoadScene("MainMenu");

    }
    public void NoButtonClick()
    {
        ExitToMenuPanel.SetActive(false);
    }

    [System.Serializable]
    public class AwardRuntime
    {
        public string AwardName;
        public string PlayerPrefsVariable;
        public int Points;
        public GameObject AwardObject;
    }
}