using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public Button NewGameButton;
    public Button InfoButton;
    public Button HighscoresButton;
    public Button SettingsButton;
    public Button ExitButton;
    public GameObject GameInfo;
    public GameObject ChooseYourPlayer;
    public GameObject HighscoreTableScreen;
    public Button MenuButton;
    public Button TutorialButton;
    public Button SettingsMenuButton;
    public Button NewGameButtonInfo;
    public Button AwardsButton;
    public Button SwitchCharacter;
    public GameObject SettingsPanel;
    public GameObject AwardsPanel;
    public GameObject InputYourNamePanel;
    public GameObject PlayerGirl;
    public GameObject PlayerBoy;
    public Slider EffSlider;
    public Slider MusSlider;
    public Slider MasSlider;
    public AudioMixer MasterMixer;
    public TextMeshProUGUI BestScoreText;
    public int IsTutorialCompleted;



    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        AwardsPanel.SetActive(false);
        GameInfo.SetActive(false);
        HighscoreTableScreen.SetActive(false);
        SettingsPanel.transform.gameObject.SetActive(false);
        NewGameButton.onClick.AddListener(NewGameButtonClick);
        NewGameButtonInfo.onClick.AddListener(NewGameButtonClick);
        InfoButton.onClick.AddListener(InfoButtonClick);
        HighscoresButton.onClick.AddListener(HighscoresButtonClick);
        SettingsButton.onClick.AddListener(SettingsButtonClick);
        ExitButton.onClick.AddListener(ExitButtonClick);
        MenuButton.onClick.AddListener(MenuButtonClick);
        TutorialButton.onClick.AddListener(TutorialButtonClick);
        SettingsMenuButton.onClick.AddListener(SettingsMenuButtonClick);
        AwardsButton.onClick.AddListener(AwardsButtonClick);
        SwitchCharacter.onClick.AddListener(SwitchCharacterButtonClick);
        ChooseYourPlayer.SetActive(false);
        InputYourNamePanel.SetActive(false);

        if (PlayerPrefs.GetString("PlayerName") == null || PlayerPrefs.GetString("PlayerName") == "") //  If this is the first launch of the game, or the player’s name is not set, a name input panel will appear
        {
            InputYourNamePanel.SetActive(true);
        }

        //Shows the selected character on the main screen
        if (PlayerPrefs.GetString("PlayerType") == "Boy")
        {
            PlayerGirl.SetActive(false);
        }
        else
        {
            PlayerBoy.SetActive(false);
        }

        //Shows the best result of the player, if it is not equal to zero
        if (PlayerPrefs.GetInt("BestScore") > 0)
        {
            BestScoreText.text = "Highscore: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            BestScoreText.text = " ";
        }

        //Shows the highscore panel if the menu starts after the game is over.
        if (PlayerPrefs.GetInt("IsHighscoreScreenOn") == 1)
        {
            PlayerPrefs.SetInt("IsHighscoreScreenOn", 0);
            HighscoreTableScreen.SetActive(true);

        }

    }
    //Shows the selected character on the main screen, change it
    void SwitchCharacterButtonClick()
    {
        if (PlayerPrefs.GetString("PlayerType") == "Boy")
        {
            PlayerPrefs.SetString("PlayerType", "Girl");
            PlayerBoy.SetActive(false);
            PlayerGirl.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("PlayerType", "Boy");
            PlayerGirl.SetActive(false);
            PlayerBoy.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //close setting panel after tscape button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsPanel.active)
            {
                SettingsPanel.SetActive(false);
            }

        }
    }

    void NewGameButtonClick()
    {

        // If the tutorial is completed, the game starts.If not, the tutorial starts.
        IsTutorialCompleted = PlayerPrefs.GetInt("IsCompleted");
        if (IsTutorialCompleted == 0)
        {
            SceneManager.LoadScene("TutorialLevelScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void InfoButtonClick()
    {
        GameInfo.SetActive(true);
    }



    void HighscoresButtonClick()
    {

        HighscoreTableScreen.SetActive(true);
    }
    void TutorialButtonClick()
    {
        SceneManager.LoadScene("TutorialLevelScene");
    }

    void SettingsButtonClick()
    {
        //Getting the same volume level that was set previously
        MusSlider.value = GetLevels("MusicVol");
        EffSlider.value = GetLevels("EffVol");
        MasSlider.value = GetLevels("MasterVol");
        SettingsPanel.SetActive(true);

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
    // buttons that close or open different menu items
    void SettingsMenuButtonClick()
    {
        SettingsPanel.SetActive(false);
    }

    void MenuButtonClick()
    {
        GameInfo.SetActive(false);
    }

    void ExitButtonClick()
    {
        Application.Quit();
    }
    void AwardsButtonClick()
    {
        AwardsPanel.SetActive(true);
    }
}
