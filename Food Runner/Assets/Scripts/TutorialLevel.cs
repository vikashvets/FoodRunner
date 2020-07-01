using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;


public class TutorialLevel : MonoBehaviour
{
    public TextMeshProUGUI InPanelInfoText;
    public TextMeshProUGUI OutPanelInfoText;
    public GameObject OutPanel;
    public GameObject InPanel;
    public GameObject UsefulItem;
    public GameObject JunkItem;
    public GameObject Arrow;
    public GameObject MovingArrow;
    public GameObject Monster;
    public GameObject Player;
    private GameObject[] Arrows;
    float TimeBtwSpawn = 3f;
    float StartTimeBtwSpawn = 3f;
    private LevelManager GameLevelManager;
    public GameObject BlackEmphPanelIn;
    public GameObject BlackEmphPanelOut;
    public GameObject ScoreText;
    public GameObject BestScoreText;
    public TextMeshProUGUI BestScoreTextText;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public Button NextButtonInPanel;
    public Button SkipButton;
    public Button NextButtonOutPanel;
    public Button DownButton;
    public GameObject NextButtonOutPanelGameObject;
    public GameObject NextButtonInPanelGameObject;
    public Button EndTutorialMenu;
    public Button EndTutorialPlay;
    public LifeController GameLifeController;
    public int PressCount = 0;
    public bool Step1Performed = false;
    public bool Step2Performed = false;
    public bool Step3Performed = false;
    public bool Step4Performed = false;
    public bool Step5Performed = false;
    public bool Step6Performed = false;
    public bool Step7Performed = false;
    public bool Step8Performed = false;
    public bool Step9Performed = false;
    public bool Step10Performed = false;
    public bool Step11Performed = false;
    public bool Step12Performed = false;
    public bool Step13Performed = false;
    public bool CoroutinePerformed = false;
    GameObject PlayerArrow = null;
    GameObject MonsterArrow = null;
    GameObject ScoreArrow = null;
    GameObject LifeArrow = null;
    GameObject DownButtonArrow = null;
    public GameObject TutorialCompletedScreen;
    public Canvas canvas;
    public GameObject ForTutorialLevel;
    public GameObject BlackAwardPanel;
    public GameObject TutorialPlatformSpawner;
    // Start is called before the first frame update
    void Start()
    {
        Arrow.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        MovingArrow.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        Monster.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
        TutorialPlatformSpawner.SetActive(false);
        BestScoreTextText.text = "Highscore: " + PlayerPrefs.GetInt("BestScore").ToString();
        ScoreText.SetActive(false);
        BestScoreText.SetActive(false);
        TutorialCompletedScreen.SetActive(false);
        //Duplicates to highlight during the game
        Life1.SetActive(false);
        Life2.SetActive(false);
        Life3.SetActive(false);
        NextButtonInPanel.onClick.AddListener(NextButtonClick);
        EndTutorialMenu.onClick.AddListener(EndTutorialMenuClick);
        EndTutorialPlay.onClick.AddListener(EndTutorialPlayClick);
        NextButtonOutPanel.onClick.AddListener(NextButtonClick);
        SkipButton.onClick.AddListener(SkipButtonClick);
        GameLevelManager = FindObjectOfType<LevelManager>();
        GameLifeController = FindObjectOfType<LifeController>();
        BlackEmphPanelIn.SetActive(false);
        TextChange("Hi, this is the Food Runner tutorial! Try to collect as many points as you can!", OutPanelInfoText);


    }

    // Update is called once per frame
    /*
    Some of the steps end after a specific player action, others after a button is pressed. 
    Here the required number of clicks and steps taken is checked. 
    Each step is a small element of the sequence of the tutorial.*/
    void Update()
    {
        if (PressCount == 1 && !Step1Performed)
        {
            Step1();
            Step1Performed = true;
        }

        else if (PressCount == 2 && !Step2Performed)
        {
            Step2();
            Step2Performed = true;
        }
        else if (PressCount == 3 && !Step3Performed)
        {
            Step3();
            Step3Performed = true;
        }

        else if (Step3Performed && !Step4Performed)
        {
            Step4();
        }

        else if (Step4Performed && !Step5Performed)
        {
            Step5();
        }
        else if (Step5Performed && !Step6Performed)
        {
            Step6();
        }
        else if (PressCount == 4 && !Step7Performed)
        {
            Step7();
        }
        else if (PressCount == 5 && !Step8Performed)
        {
            Step8();
        }
        else if (Step8Performed && !Step9Performed)
        {
            Step9();
        }
        else if (PressCount == 6 && !Step10Performed)
        {
            Step10();
        }
        else if (PressCount == 7 && !Step11Performed)
        {
            Step11();
        }
        else if (Step11Performed && !Step12Performed)
        {
            Step12();
        }
        else if (Step12Performed && !Step13Performed)
        {
            Step13();
        }
        else if (Step13Performed)
        {
            PlayerPrefs.SetInt("IsCompleted", 1);
            // SceneManager.LoadScene("MainMenu");
            // An additional check so as not to change the already established result.
            if (PlayerPrefs.GetInt("IsTutorialAwardReceived") != 1)
            {
                PlayerPrefs.SetInt("IsTutorialAwardReceived", 1);
                StartCoroutine(EndTutorialRunWithAward());
            }
            else
            {
                StartCoroutine(EndTutorialRunWithoutAward());
            }
        }


    }

    public void TextChange(string CurrentText, TextMeshProUGUI Panel)
    {
        Panel.text = CurrentText;
    }
    //If the player has completed the tutorial to the end, he will receive a reward, but if he pressed the skip key, then no.
    IEnumerator EndTutorialRunWithAward()
    {
        yield return new WaitForSeconds(3);
        TutorialCompletedScreen.SetActive(true);
        BlackAwardPanel.SetActive(true);
        var Award = Instantiate(ForTutorialLevel, new Vector3(0, 20, 0), Quaternion.identity) as GameObject;
        Award.transform.SetParent(canvas.transform, false);
        StartCoroutine(DestroyAward(Award));
    }

    IEnumerator EndTutorialRunWithoutAward()
    {
        yield return new WaitForSeconds(3);
        TutorialCompletedScreen.SetActive(true);
    }
    IEnumerator DestroyAward(GameObject Award)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(Award);
        BlackAwardPanel.SetActive(false);
    }
    void NextButtonClick()
    {
        PressCount++;
    }
    void SkipButtonClick()
    {
        //Changing the sort layers so that all this does not protrude beyond the screen for completing the tutorial when you press the skip button.
        Arrow.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        MovingArrow.GetComponent<SpriteRenderer>().sortingLayerName = "Backgroung";
        Monster.GetComponent<SpriteRenderer>().sortingLayerName = "Backgroung";
        //Cleaning the screen of objects that are no longer needed
        Arrows = GameObject.FindGameObjectsWithTag("arrow");
        for (int i = 0; i < Arrows.Length; i++)
        {
            Destroy(Arrows[i]);
        }
        //The player has completed the tutorial but does not receive a reward
        if (PlayerPrefs.GetInt("IsTutorialAwardReceived") != 1)
        {
            PlayerPrefs.SetInt("IsTutorialAwardReceived", 0);
        }
        PlayerPrefs.SetInt("IsCompleted", 1);
        TutorialCompletedScreen.SetActive(true);
    }

    void EndTutorialMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void EndTutorialPlayClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void Step1()
    {
        // Highlighting the desired panel.
        BlackEmphPanelOut.SetActive(false);
        NextButtonOutPanelGameObject.SetActive(false);
        OutPanel.SetActive(false);
        BlackEmphPanelIn.SetActive(true);
        //setting the pointing arrow on player
        TextChange("This is the main character.You control him with taps.", InPanelInfoText);
        PlayerArrow = Instantiate(Arrow, new Vector3(-1.4f, 1, 0), Quaternion.identity) as GameObject;
    }

    void Step2()
    {
        Destroy(PlayerArrow);
        //setting the pointing arrow on monster
        TextChange("This is a monster. He is chasing you and wants to eat you!But this tutorial will teach you how to prevent this.", InPanelInfoText);
        MonsterArrow = Instantiate(Arrow, new Vector3(-6.35f, 1, 0), Quaternion.identity) as GameObject;
    }

    void Step3()
    {
        BlackEmphPanelIn.SetActive(false);
        NextButtonOutPanelGameObject.SetActive(true);
        OutPanel.SetActive(true);
        Destroy(MonsterArrow);
        TextChange("Ok, lets get started! Tap anywhere on the screen to jump. Two taps - double jump.", OutPanelInfoText);
        NextButtonOutPanelGameObject.SetActive(false);
    }

    void Step4()
    {

        // NextButtonInPanelGameObject.SetActive(false);
        //for desktop
        /* if (Input.GetButtonDown("Jump"))
         {
             Step4Performed = true;
         }*/
        //Waiting for a player to tap
        if (Input.touchCount > 0 & Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Step4Performed = true;
        }


    }

    void Step5()
    {   //  Spawn of useful food, waiting for the player to catch it
        TextChange("Good job! Now try to collect useful food. Healthy food id highlighted in green. Jump using tap to catch it!", OutPanelInfoText);
        if (TimeBtwSpawn <= 0)
        {
            Instantiate(MovingArrow, new Vector3(10, 3, 0), Quaternion.identity);
            Instantiate(UsefulItem, new Vector3(10, 0, 0), Quaternion.identity);
            TimeBtwSpawn = StartTimeBtwSpawn;
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }
        if (GameLevelManager.score > 0)// If the score has changed, then the player has caught food
        {
            //Cleaning the screen of objects that are no longer needed
            Arrows = GameObject.FindGameObjectsWithTag("arrow");
            for (int i = 0; i < Arrows.Length; i++)
            {
                Destroy(Arrows[i]);
            }
            Step5Performed = true;
            TimeBtwSpawn = StartTimeBtwSpawn;
        }


    }

    void Step6()
    {
        NextButtonOutPanelGameObject.SetActive(true);
        ScoreText.SetActive(true);
        BestScoreText.SetActive(true);
        BlackEmphPanelOut.SetActive(true);
        TextChange("Awesome! As you can see, you score has changed. Points were added to the score for a useful item, collected by you!", OutPanelInfoText);
        ScoreArrow = Instantiate(Arrow, new Vector3(2, 2f, 0), Quaternion.identity) as GameObject;
        ScoreArrow.transform.Rotate(180, 0, 40, Space.Self);
        Step6Performed = true;
    }

    void Step7()
    {
        Destroy(ScoreArrow);
        ScoreText.SetActive(false);
        BestScoreText.SetActive(false);
        TextChange("Useful items give you 10 points, and a set of useful items - 20 points. Sets are highlighted in gold.", OutPanelInfoText);
        Step7Performed = true;
    }
    void Step8()
    {
        NextButtonOutPanelGameObject.SetActive(false);
        BlackEmphPanelOut.SetActive(false);
        TextChange("Now lets see what happens if you pick up the junk food. Junk food is highlighted in red. Try to catch it! ", OutPanelInfoText);

        if (TimeBtwSpawn <= 0)//  Spawn of junk food, waiting for the player to catch it
        {
            Instantiate(MovingArrow, new Vector3(10, 3, 0), Quaternion.identity);
            Instantiate(JunkItem, new Vector3(10, 0, 0), Quaternion.identity);
            TimeBtwSpawn = StartTimeBtwSpawn;
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }
        if (GameLifeController.hearts.Count == 3)// If the life count has changed, then the player has caught junk food
        {
            //Cleaning the screen of objects that are no longer needed
            Arrows = GameObject.FindGameObjectsWithTag("arrow");
            for (int i = 0; i < Arrows.Length; i++)
            {
                Destroy(Arrows[i]);

            }
            TimeBtwSpawn = StartTimeBtwSpawn;
            Step8Performed = true;

        }


    }
    void Step9()
    {
        //Highlighting a monster and lives
        NextButtonOutPanelGameObject.SetActive(true);
        Monster.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        Life1.SetActive(true);
        Life2.SetActive(true);
        Life3.SetActive(true);
        BlackEmphPanelOut.SetActive(true);
        TextChange("Oh no! Catching junk food took you one life and speed up the monster!", OutPanelInfoText);
        MonsterArrow = Instantiate(Arrow, new Vector3(-4.5f, -0.5f, 0), Quaternion.identity) as GameObject;
        MonsterArrow.transform.Rotate(0, 0, -23, Space.Self);
        LifeArrow = Instantiate(Arrow, new Vector3(-3.5f, 3.6f, 0), Quaternion.identity) as GameObject;
        LifeArrow.transform.Rotate(0, 0, -90, Space.Self);
        Step9Performed = true;

    }

    void Step10()
    {
        Destroy(LifeArrow);
        Destroy(MonsterArrow);
        Monster.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
        Life1.SetActive(false);
        Life2.SetActive(false);
        Life3.SetActive(false);
        BlackEmphPanelOut.SetActive(true);
        TextChange("Remember, that when life ends and the monster will catch you, the game will be over. Try not to pick up junk food!", OutPanelInfoText);
        Step10Performed = true;
    }

    void Step11()
    {
        if (!CoroutinePerformed)//  Waiting for the player to jump off the platform.
        {
            NextButtonOutPanelGameObject.SetActive(false);
            BlackEmphPanelOut.SetActive(false);
            TextChange("The button at the bottom of the screen is made for you can jump off it to avoid colliding with junk food or have time to pick up useful food. Try to do it!", OutPanelInfoText);
            TutorialPlatformSpawner.SetActive(true);//Platform spawn start
            Player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(StartPlatformSpawn());

        }

    }
    void Step12()
    {
        NextButtonOutPanelGameObject.SetActive(false);
        BlackEmphPanelOut.SetActive(false);
        TextChange("Awesome! Now catch another one health food and you ready to start the game!", OutPanelInfoText); //  Spawn of useful food, waiting for the player to catch it

        if (TimeBtwSpawn <= 0)
        {
            Instantiate(MovingArrow, new Vector3(10, 3, 0), Quaternion.identity);
            Instantiate(UsefulItem, new Vector3(10, 0, 0), Quaternion.identity);
            TimeBtwSpawn = StartTimeBtwSpawn;
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }
        if (GameLevelManager.score > 10)
        {
            Arrows = GameObject.FindGameObjectsWithTag("arrow");
            for (int i = 0; i < Arrows.Length; i++)
            {
                Destroy(Arrows[i]);
            }
            Step12Performed = true;
            TimeBtwSpawn = StartTimeBtwSpawn;
        }

    }
    void Step13()
    {
        TextChange("Cool! You are ready to play!", OutPanelInfoText);
        Step13Performed = true;
    }


    IEnumerator StartPlatformSpawn()
    {//Transfer a player to the platform
        CoroutinePerformed = true;
        yield return new WaitForSeconds(4);
        DownButtonArrow = Instantiate(Arrow, new Vector3(-7f, -1.5f, 0), Quaternion.identity) as GameObject;
        DownButtonArrow.transform.Rotate(-180, -1.3f, -150, Space.Self);
        DownButton.onClick.AddListener(DownButtonClick);
        Player.transform.position = new Vector3(-1.6f, 2, 10);
    }
    public void DownButtonClick()
    {
        Step11Performed = true;
        TutorialPlatformSpawner.SetActive(false);
        // Removing platforms after a player jumps from them
        var Platforms = GameObject.FindGameObjectsWithTag("platform");
        for (int i = 0; i < Platforms.Length; i++)
        {
            Destroy(Platforms[i]);

        }
        Arrows = GameObject.FindGameObjectsWithTag("arrow");
        for (int i = 0; i < Arrows.Length; i++)
        {
            Destroy(Arrows[i]);
        }
    }

}