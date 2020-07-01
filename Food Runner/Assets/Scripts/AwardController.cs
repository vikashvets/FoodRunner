using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AwardController : MonoBehaviour
{
    // Awards button and game objects in award panel
   
    public GameObject BlackPanel;
    public GameObject canvas;
    private GameObject CurrentAward = null;
    public Button CloseButton;
    public TextMeshProUGUI AwardText;
    public Button MenuButton;
    // Start is called before the first frame update
    public Award[] Awards;
    public Award PressedObject;
    void Start()
    {
        CloseButton.onClick.AddListener(CloseButtonClick);
        MenuButton.onClick.AddListener(MenuButtonClick);
        BlackPanel.SetActive(false);
        foreach (Award Sample in Awards)
        {
            Sample.AwardObject.GetComponent<Button>().onClick.AddListener(AwardButtonClick);
        }
        foreach (Award Sample in Awards)
        {
            if (PlayerPrefs.GetInt(Sample.PlayerPrefsVariable) == 0) // If this award is not received, paint it dark
            {
                
                var AwardImage = Sample.AwardObject.transform.Find("Image").GetComponent<Image>();
                AwardImage.color = new Color32(49, 49, 49, 255);
                Sample.AwardObject.SetActive(true);
            }
        }

    }
    void AwardButtonClick()
    {
        string nameButton = EventSystem.current.currentSelectedGameObject.name;

        foreach (Award Sample in Awards)
        {
            if (Sample.AwardName == nameButton)
            {
                PressedObject = Sample;
            }
        }

        BlackPanel.SetActive(true);// Make black panel visible
        var LocalAward = Instantiate(PressedObject.AwardObject, new Vector3(0, 80, 0), Quaternion.identity);//Show this award
        LocalAward.GetComponent<Button>().enabled = false; // Disable clicking on this button
        LocalAward.GetComponent<Button>().transform.SetParent(canvas.transform, false);
        CurrentAward = LocalAward.gameObject;
        if (PlayerPrefs.GetInt(PressedObject.PlayerPrefsVariable) == 0) // Different texts that depend on whether the award is received
        {
            AwardText.text = PressedObject.DescriptionAwardNotReceived;
        }
        else
        {
            AwardText.text = PressedObject.DescriptionAwardReceived;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If device escape button pressed
        {
            if (BlackPanel.active)//  If a certain award is open on a black screen, close it using the device button
            {
                Destroy(CurrentAward);
                BlackPanel.SetActive(false);

            }
            else if (canvas.active)// If the award panel is open, close it using the device button
            {
                canvas.SetActive(false);
            }

        }
    }
    
    
    void CloseButtonClick() // Back to main award screen
    {
        Destroy(CurrentAward);
        BlackPanel.SetActive(false);
    }

    void MenuButtonClick() // Back to menu
    {
        canvas.SetActive(false);
        Debug.Log("Exit");
    }
    
    [System.Serializable]
    public class Award
    {
        public string AwardName;
        public string PlayerPrefsVariable;
        public string DescriptionAwardReceived;
        public string DescriptionAwardNotReceived;
        public GameObject AwardObject;
    }
}
