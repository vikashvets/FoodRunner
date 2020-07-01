using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputYourName : MonoBehaviour
{
    
    public GameObject WarningPanel;
    public GameObject InputYourNamesScreen;
    public GameObject ChooseYourePlayerScreen;
    public TMP_InputField PlayerName;
    private bool IsPermitted = false; // A variable that indicates whether a player’s name can be saved
    public Button ContinueButton;
    public Button CloseButton;
    public Button PlayerGirlButton;
    public Button PlayerBoyButton;
    public GameObject PlayerGirlImage;
    public GameObject PlayerBoyImage;
    public Button PlayerContinueButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayerName.onEndEdit.AddListener(SubmitName);
        ContinueButton.onClick.AddListener(ContinueButtonClick);
        PlayerBoyButton.onClick.AddListener(PlayerBoyButtonClick);
        PlayerGirlButton.onClick.AddListener(PlayerGirlButtonClick);
        CloseButton.onClick.AddListener(CloseButtonClick);
        PlayerContinueButton.onClick.AddListener(PlayerContinueButtonClick);
        ChooseYourePlayerScreen.SetActive(false);
        WarningPanel.transform.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SubmitName(string name)//Validation of a player’s name, can be strengthened by additional restrictions
    {
        if (name != null && !name.Contains(" ") && name != "")
        {
            PlayerPrefs.SetString("PlayerName", PlayerName.text);
            IsPermitted = true; //Name is good, player can resume

        }
        else
        {
            name = "";
            IsPermitted = false; // something wrong

        }

    }

    void ContinueButtonClick()
    {
        if (IsPermitted)
        {
            InputYourNamesScreen.SetActive(false);
            ChooseYourePlayerScreen.SetActive(true);// Loading next screen

        }
        else
        {
            WarningPanel.transform.gameObject.SetActive(true);//Warning panel activation
            IsPermitted = false;

        }

    }

    void CloseButtonClick()
    {
        WarningPanel.transform.gameObject.SetActive(false);
    }
    //Character select and displaying it in the menu screen
    void PlayerBoyButtonClick()
    {
        PlayerPrefs.SetString("PlayerType", "Boy");
        PlayerGirlImage.SetActive(false);
        PlayerBoyImage.SetActive(true);
    }

    void PlayerGirlButtonClick()
    {
        PlayerPrefs.SetString("PlayerType", "Girl");
        Debug.Log(PlayerPrefs.GetString("PlayerType"));
        PlayerBoyImage.SetActive(false);
        PlayerGirlImage.SetActive(true);
    }

    void PlayerContinueButtonClick()
    {
        ChooseYourePlayerScreen.SetActive(false);
    }

}
