using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class EndGameScreen : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public Button ContinueButton;
    public Canvas canvas;
    public GameObject ForTheBestHighscoreLocalObject;
    public GameObject BlackAwardPanel;
    public GameObject NoNetwork;
    public GameObject IntrestingTips;
    public Button YesButton;
    public Button NoButton;
    private bool IsBackPermitted = true; //For exit with devices buttons

    // Start is called before the first frame update
    void Start()
    {
        BlackAwardPanel.SetActive(false);
        IntrestingTips.SetActive(true);
        ScoreText.text = PlayerPrefs.GetInt("score").ToString(); //Player score after game
        PlayerPrefs.SetInt("IsAnyGamePlayed", 1);
        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("BestScore") && PlayerPrefs.GetInt("IsLocalAwardReceived") != 1)// If the award has not yet been received and this is the first set record on the device
        {
            PlayerPrefs.SetInt("IsLocalAwardReceived", 1);// We establish that the award is received(to receive the award only for the first time)
            IsBackPermitted = false;//  We block the actions of the device button while the award image is active
            var Award = Instantiate(ForTheBestHighscoreLocalObject, new Vector3(0, 20, 0), Quaternion.identity) as GameObject;
            Award.transform.SetParent(canvas.transform, false);
            BlackAwardPanel.SetActive(true);
            StartCoroutine(DestroyAward(Award));//Destroy award after 5 sec
        }
        NoNetwork.SetActive(false);
        ContinueButton.onClick.AddListener(ContinueButtonClick);
        YesButton.onClick.AddListener(YesButtonClick);
        NoButton.onClick.AddListener(NoButtonClick);
        HighscoreTable.AddHighscoreEntry(PlayerPrefs.GetInt("score"), System.DateTime.Now.ToString("dd/MM/yyyy")); // Writing a player result to a local score table
    }
    IEnumerator DestroyAward(GameObject Award)
    {

        yield return new WaitForSeconds(5);
        IsBackPermitted = true; // Set that back device button can be used now
        BlackAwardPanel.SetActive(false);
        Destroy(Award);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (NoNetwork.active)
            {
                if (IsBackPermitted) // if back device button can be used now
                {
                    NoNetwork.SetActive(false);
                }
            }
            else if (IntrestingTips.active)
            {
                if (IsBackPermitted)
                {
                    IntrestingTips.SetActive(false);
                }
            }

        }
    }



    void ContinueButtonClick()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable) // check for network, if not reachable activating NoNetwork warning panel
        {
            NoNetwork.SetActive(true);
        }
        else 
        {
            //Upload highscore to the remote highscores table
            StartCoroutine(WorldHighscoreTable.UploadNewHighscore(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("score"), 1));
            PlayerPrefs.SetInt("IsHighscoreScreenOn", 1); // Indication that after going to the menu scene, it is needed to open the high score table panel
            SceneManager.LoadScene("MainMenu");//loading main menu

        }

    }

    // buttons on warning panel
    void YesButtonClick()
    {
        PlayerPrefs.SetInt("IsHighscoreScreenOn", 1);// Indication that after going to the menu scene, it is needed to open the high score table panel
        SceneManager.LoadScene("MainMenu");
    }

    void NoButtonClick()
    {
        NoNetwork.SetActive(false);
    }


}
